using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : CombatCharacter
{
    [SerializeField] private int CON = 10;
    [SerializeField] private int INT = 10;
    [SerializeField] private int DEX = 10;
    [SerializeField] private int statPoints = 0;

    public Weapon weapon;
    public Armor armor;

    private Ability ability1 = new Ability(2);
    private Ability ability2 = new Ability(8);
    private Ability ability3 = new Ability(5);
    private Ability ability4 = new Ability(10);

    [SerializeField] int xpPerKill = 20;

    [SerializeField] float speed = 5f;
    [SerializeField] float sprintingSpeed = 10f;
    [SerializeField] GameObject ability1Effect;
    [SerializeField] GameObject ability2Effect;
    [SerializeField] BoxCollider rangeCollider;

    [SerializeField] GameObject playerInfoPanel;
    [SerializeField] GameObject inventory;
    [SerializeField] GameObject characterStatsPanel;
    [SerializeField] GameObject youDiedPanel;
    
    Animator animator;
    RangeSensor rangeSensor;

    int damage;
    public int maxHealth = 1;

    private int xpPoints = 0;
    private int level = 1;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rangeSensor = GetComponentInChildren<RangeSensor>();
        calculateHealth();
        health = maxHealth;
        calculateDamage();
        updateStatsPanel();
        updatePlayerInfo();
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
        if (!Input.GetKey(KeyCode.LeftAlt))
        {
            transform.Rotate(transform.up, mouseInput.x);
        }

        //animate
        animator.SetFloat("Horizontal", horizontal, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);

        //Abilities
        if (Input.GetKeyDown(KeyCode.Alpha1) && ability1.Cast())
        {
            if (rangeSensor.enemies.Count > 0)
            {
                animator.SetTrigger("Cast");
                Golem target = rangeSensor.enemies[0];
                if (target.health - damage <= 0)
                    rangeSensor.enemies.RemoveAt(0);
                Instantiate(ability1Effect, target.transform.position, Quaternion.identity);
                target.TakeDamage(damage);
                if (target.IsDead)
                {
                    addXP(xpPerKill);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && ability2.Cast())
        {
            animator.SetTrigger("Cast");
            heal(damage / 2);
            Instantiate(ability2Effect, transform.position, Quaternion.identity).transform.parent = transform;
        }

        // Toggle inventory visibility
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.GetComponent<RectTransform>().anchoredPosition.x < 1200)
            {
                inventory.GetComponent<RectTransform>().anchoredPosition += Vector2.right * 500;
            }
            else
            {
                inventory.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * 500;
            }
        }

        // Toggle character stats panel visibility
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (characterStatsPanel.GetComponent<RectTransform>().anchoredPosition.x < 1200)
            {
                characterStatsPanel.GetComponent<RectTransform>().anchoredPosition += Vector2.right * 500;
            }
            else
            {
                characterStatsPanel.GetComponent<RectTransform>().anchoredPosition -= Vector2.right * 500;
            }
        }

    }

    private void addXP(int amount)
    {
        int milestones = xpPoints / 200;
        xpPoints += amount;
        if (xpPoints / 200 > milestones)
        {
            levelUp();
        }
        updatePlayerInfo();
    }

    protected override void Die()
    {
        youDiedPanel.SetActive(true);
        animator.SetTrigger("Die");
        Invoke(nameof(resetScene), 5f);
    }

    private void resetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene1");
    }

    private void levelUp()
    {
        level++;
        statPoints += 5;
        updateStatsPanel();
    }

    public void heal(int amount)
    {
        health += amount;
        health = health > maxHealth ? maxHealth : health;
    }

    public void applyItem(Item item)
    {
        if (item is Weapon)
        {
            if (weapon)
            {
                inventory.GetComponent<Inventory>().addItem(weapon);
            }
            weapon = item as Weapon;
        }
        else if (item is Armor)
        {
            if (armor)
            {
                inventory.GetComponent<Inventory>().addItem(armor);
            }
            armor = item as Armor;
        }
        calculateDamage();
        calculateHealth();
        updateStatsPanel();
    }

    public void incrementCON()
    {
        incrementStats(1, 0, 0);
        health += 6;
    }

    public void incrementINT()
    {
        incrementStats(0, 1, 0);
    }

    public void incrementDEX()
    {
        incrementStats(0, 0, 1);
    }

    public void incrementStats(int CON, int INT, int DEX)
    {
        this.CON += CON;
        this.INT += INT;
        this.DEX += DEX;

        this.statPoints -= CON + INT + DEX;

        calculateHealth();
        calculateDamage();

        updateStatsPanel();
    }

    private void calculateHealth()
    {
        maxHealth = CON * 6 + (armor ? armor.getHealth() : 0);
    }

    private void calculateDamage()
    {
        damage = INT * 3 + (weapon ? weapon.getAttack() : 0);
    }

    private void updateStatsPanel()
    {
        var panel = characterStatsPanel.GetComponent<CharacterStatsWindow>();
        panel.setHealth(maxHealth);
        panel.setDamage(damage);
        panel.setStatPoints(statPoints);
        panel.setCON(CON);
        panel.setINT(INT);
        panel.setDEX(DEX);
    }

    private void updatePlayerInfo()
    {
        var panel = playerInfoPanel.GetComponent<CharacterInfoPanelLogic>();
        panel.setName(Name);
        panel.setLevel(level);
        panel.setXP(xpPoints);
    }

}
