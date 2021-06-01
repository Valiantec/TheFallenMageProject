using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterStatsWindow : MonoBehaviour
{
    [SerializeField] private GameObject buttonsPanel;

    [SerializeField] private TMP_Text health;
    [SerializeField] private TMP_Text damage;
    [SerializeField] private TMP_Text CON;
    [SerializeField] private TMP_Text INT;
    [SerializeField] private TMP_Text DEX;
    [SerializeField] private TMP_Text statPoints;

    public void setHealth(int amount)
    {
        health.text = amount.ToString();
    }

    public void setDamage(int amount)
    {
        damage.text = amount.ToString();
    }

    public void setCON(int amount)
    {
        CON.text = amount.ToString();
    }

    public void setINT(int amount)
    {
        INT.text = amount.ToString();
    }

    public void setDEX(int amount)
    {
        DEX.text = amount.ToString();
    }

    public void setStatPoints(int amount)
    {
        if (amount > 0)
        {
            buttonsPanel.SetActive(true);
        }
        else
        {
            buttonsPanel.SetActive(false);
        }
        statPoints.text = amount.ToString();
    }
}
