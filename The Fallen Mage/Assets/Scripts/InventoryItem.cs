using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] Sprite sprite;

    private Image imageComponent;

    void Start()
    {
        imageComponent = GetComponentsInChildren<Image>()[1];
        imageComponent.sprite = sprite;
    }


    public void SetTexture(Sprite sprite)
    {
        this.sprite = sprite;
    }
}
