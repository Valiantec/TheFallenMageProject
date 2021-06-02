using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] GameObject itemGrid;

    public GameObject weaponSlot;
    public GameObject armorSlot;

    public void addItem(Item item)
    {
        item.gameObject.transform.SetParent(itemGrid.transform);
        item.gameObject.transform.localScale = Vector3.one;
    }

    public void removeItem(Item item)
    {
        Destroy(item.gameObject);
    }
}
