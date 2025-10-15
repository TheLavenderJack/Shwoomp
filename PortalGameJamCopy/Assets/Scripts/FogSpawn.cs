using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogSpawn : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float spawnInterval = 1f;
     [SerializeField] private float horizontalSpawnRange = 1f;
    private float timer;

    


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        float xOffset = Random.Range(-horizontalSpawnRange, horizontalSpawnRange);
        Vector3 spawnPosition = transform.position + new Vector3(xOffset, 0f, 0f);

        Instantiate(bubblePrefab, spawnPosition, Quaternion.identity);
    }
}
