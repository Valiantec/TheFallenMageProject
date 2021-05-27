using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Player player;

    void Update()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector2(player.health / ((float)player.maxHealth) * 205, 10);
    }
}
