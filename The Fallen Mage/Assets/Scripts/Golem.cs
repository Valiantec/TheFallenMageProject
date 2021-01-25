using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Golem : MonoBehaviour
{

    [SerializeField] int maxHealth = 100;
    public int health;

    GolemSpawner spawner;
    
    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        
    }

    public void SetSpawner(GolemSpawner spawner)
    {
        this.spawner = spawner;
    }

    void Die()
    {
        spawner.DecrementGolemCount();
        Destroy(gameObject);
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
            Die();
    }
}
