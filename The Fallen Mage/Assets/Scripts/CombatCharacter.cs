using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatCharacter : Character
{
    public int health;

    public bool TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        return true;
    }

    protected abstract void Die();
}
