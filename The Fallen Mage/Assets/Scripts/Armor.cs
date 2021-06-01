using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Item, Enhanceable
{
    [SerializeField] private int defense;

    public void enhance()
    {
        defense = (int)(defense * 1.2f);
    }
}
