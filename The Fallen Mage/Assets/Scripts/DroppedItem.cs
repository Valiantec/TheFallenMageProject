using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    public GameObject item;

    private GameObject player;
    private Inventory inventory;

    private float baseY;
    private float y;

    void Start()
    {
        baseY = transform.position.y;
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = GameObject.FindGameObjectWithTag("Canvas").GetComponentInChildren<Inventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 5f)
            {
                inventory.addItem(Instantiate(item).GetComponent<Item>());
                Destroy(gameObject);
            }
        }


        y += 0.01f;
        if (y > 180)
        {
            y = 0;
        }

        transform.position = new Vector3(transform.position.x,
            baseY + Mathf.Sin(y) * 0.2f + 0.6f,
            transform.position.z);
    }
}
