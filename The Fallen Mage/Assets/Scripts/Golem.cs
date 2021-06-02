using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Golem : CombatCharacter
{
    [SerializeField] private int attack = 10;
    [SerializeField] private float speed = 3f;
    Rigidbody rb;
    Animator animator;

    Transform player;

    [SerializeField] GameObject spawner;

    bool attacking = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator.SetInteger("Health", health);
    }

    void FixedUpdate()
    {
        var distanceToPlayer = Vector3.Distance(transform.position, player.position);
        var distanceSpawnerToPlayer = Vector3.Distance(spawner.transform.position, player.position);
        animator.SetFloat("Vertical", 0f);
        if (distanceToPlayer > 6.5f && distanceSpawnerToPlayer < 50f - distanceToPlayer * 0.1f)
        {
            var pos = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            animator.SetFloat("Vertical", pos.z);
            rb.MovePosition(pos);
            transform.LookAt(player);
        }
        else if (!attacking && distanceToPlayer <= 6.5f)
        {
            animator.SetTrigger("Attack");
            attacking = true;
            Invoke(nameof(resetAttacking), 5f);

            player.gameObject.GetComponent<Player>().TakeDamage(attack);
        }
    }

    public void SetSpawner(GameObject spawner)
    {
        this.spawner = spawner;
    }

    protected override void Die()
    {
        animator.SetTrigger("Die");
        Invoke(nameof(DestroyGameObject), 1.2f);
    }

    private void DestroyGameObject()
    {
        Destroy(gameObject);
        GetComponent<ItemDropping>().dropItem();
        if (spawner.GetComponent<GolemSpawner>())
        {
            spawner.GetComponent<GolemSpawner>().DecrementGolemCount();
        }
    }

    private void resetAttacking()
    {
        attacking = false;
    }

}
