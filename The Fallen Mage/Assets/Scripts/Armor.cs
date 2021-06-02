using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, Enhanceable
{
    [SerializeField] private int health;

    public int getHealth()
    {
        return health;
    }

    public void enhance()
    {
        health = (int)(health * 1.2f);
    }
}
