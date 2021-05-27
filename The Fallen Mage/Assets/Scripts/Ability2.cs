using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability2 : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
}
