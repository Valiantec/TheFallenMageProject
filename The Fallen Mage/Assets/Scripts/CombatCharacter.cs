using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatCharacter : Character
{
    public int health;

    public bool TakeDamage(int amount)
    {
        if (IsDead)
            return false;

        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        return true;
    }

    public bool IsDead { get => health <= 0; }

    protected abstract void Die();
}
