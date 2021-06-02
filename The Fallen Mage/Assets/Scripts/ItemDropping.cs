using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropping : MonoBehaviour
{
    [SerializeField] List<GameObject> droppableItems = new List<GameObject>();
    [SerializeField] GameObject itemDrop;

    public void dropItem()
    {
        if (Random.Range(0, 10) < 3)
        {
            var itemToDrop = droppableItems[Random.Range(0, droppableItems.Count)];
            itemDrop.GetComponent<DroppedItem>().item = itemToDrop;
            Instantiate(itemDrop, gameObject.transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity);
        }
    }
}
