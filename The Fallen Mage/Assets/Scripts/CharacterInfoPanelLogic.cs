using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterInfoPanelLogic : MonoBehaviour
{
    [SerializeField] private TMP_Text playerName;
    [SerializeField] private TMP_Text level;
    [SerializeField] private GameObject xpBar;

    public void setName(string value)
    {
        playerName.text = value;
    }

    public void setLevel(int value)
    {
        level.text = value.ToString();
    }

    public void setXP(int value)
    {
        xpBar.GetComponent<RectTransform>().sizeDelta = new Vector2(value % 200 / ((float)200) * 105, 5.5f);
    }
}
