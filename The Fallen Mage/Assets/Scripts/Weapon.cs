using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item, Enhanceable
{
    [SerializeField] private int attack;

    public void enhance()
    {
        attack = (int)(attack * 1.2f);
    }
}
