using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemSpawner : MonoBehaviour
{
    [SerializeField] Golem golem;
    [SerializeField] float spawnInterval = 5f;
    [SerializeField] int maximumGolemCount = 5;
    int golemCount = 0;

    void Start()
    {
        StartCoroutine(SpawnGolems());
    }

    IEnumerator SpawnGolems()
    {
        while (true)
        {
            if (golemCount < maximumGolemCount)
            {
                Golem spawnedGolem = Instantiate(golem, new Vector3(transform.position.x + Random.Range(-20, 21), transform.position.y, transform.position.z + Random.Range(-20, 21)), transform.rotation);
                spawnedGolem.SetSpawner(gameObject);
                golemCount++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void DecrementGolemCount()
    {
        golemCount--;
    }
}
