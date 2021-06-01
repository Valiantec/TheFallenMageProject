using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] Player player;

    [SerializeField] GameObject itemGrid;
    [SerializeField] GameObject weaponSlot;
    [SerializeField] GameObject armorSlot;

    public void addItem(Item item)
    {
        item.gameObject.transform.SetParent(itemGrid.transform);
    }

    public void removeItem(Item item)
    {
        Destroy(item.gameObject);
    }

    //public void 
}
