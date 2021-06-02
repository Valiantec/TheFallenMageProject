using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Ability
{
    private int cooldown;
    public int Cooldown { get => cooldown; set => cooldown = value; }

    private int counter;

    public Ability(int cooldown)
    {
        this.cooldown = cooldown;
        new Thread(() =>
        {
            while (true)
            {
                Thread.Sleep(1000);
                decrementCounter();
            }
        }).Start();
    }

    private void startCooldown()
    {
        counter = cooldown;
    }

    private void decrementCounter()
    {
        counter = counter > 0 ? counter - 1 : 0;
    }

    public bool Cast()
    {
        if (counter <= 0)
        {
            startCooldown();
            return true;
        }
        else
        {
            return false;
        }
    }
}
