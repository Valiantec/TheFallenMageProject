using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogPanel;

    [SerializeField]
    private GameObject keyPanel;

    void Update()
    {
        if (keyPanel.activeSelf && Input.GetKeyDown("t"))
        {
            keyPanel.SetActive(false);
            dialogPanel.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyPanel.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyPanel.SetActive(false);
            dialogPanel.SetActive(false);
        }
    }
}
