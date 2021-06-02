using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    Player player;
    Inventory inventory;

    [SerializeField] Sprite sprite;

    private Image imageComponent;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        inventory = FindObjectOfType<Inventory>().GetComponent<Inventory>();
        imageComponent = GetComponentsInChildren<Image>()[1];
        imageComponent.sprite = sprite;
    }

    public void equip()
    {
        if (GetComponent<Weapon>())
        {
            transform.SetParent(inventory.weaponSlot.transform);
        }
        else
        {
            transform.SetParent(inventory.armorSlot.transform);
        }
        player.GetComponent<Player>().applyItem(GetComponent<Item>());
    }
}
