using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CombatCharacter
{
    [SerializeField] private int CON = 10;
    [SerializeField] private int INT = 10;
    [SerializeField] private int DEX = 10;
    [SerializeField] private int statPoints = 0;

    [SerializeField] float speed = 5f;
    [SerializeField] float sprintingSpeed = 10f;
    [SerializeField] GameObject ability1;
    [SerializeField] GameObject ability2;
    [SerializeField] BoxCollider rangeCollider;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject characterStatsPanel;
    
    Animator animator;
    RangeSensor rangeSensor;

    int damage;
    public int maxHealth = 1;

    private int level = 1;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rangeSensor = GetComponentInChildren<RangeSensor>();
        calculateHealth();
        calculateDamage();
    }

    void Update()
    {
        //take input
        bool sprinting = Input.GetKey(KeyCode.LeftShift);
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector2 mouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        //move transform
        movement *= (sprinting ? sprintingSpeed : speed) * Time.deltaTime;
        transform.Translate(movement);

        //rotate transform
        transform.Rotate(transform.up, mouseInput.x);

        //animate
        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);

        //Abilities
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (rangeSensor.enemies.Count > 0)
            {
                animator.SetTrigger("Cast");
                Golem target = rangeSensor.enemies[0];
                if (target.health - damage <= 0)
                    rangeSensor.enemies.RemoveAt(0);
                Instantiate(ability1, target.transform.position, Quaternion.identity);
                target.TakeDamage(damage);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetTrigger("Cast");
            heal(damage / 2);
            Instantiate(ability2, transform.position, Quaternion.identity).transform.parent = transform;
        }

        // Toggle inventory visibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
        }

        // Toggle character stats panel visibility
        if (Input.GetKeyDown(KeyCode.C))
        {
            characterStatsPanel.SetActive(!characterStatsPanel.activeSelf);
        }

    }

    protected override void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }

    private void levelUp()
    {
        level++;
        statPoints += 5;
    }

    public void heal(int amount)
    {
        health += amount;
        health = health > maxHealth ? maxHealth : health;
    }

    public void incrementStats(int CON, int INT, int DEX)
    {
        this.CON += CON;
        this.INT += INT;
        this.DEX += DEX;

        calculateHealth();
        calculateDamage();
    }

    private void calculateHealth()
    {
        health = CON * 6;
        maxHealth = health;
    }

    private void calculateDamage()
    {
        damage = INT * 3;
    }

}
