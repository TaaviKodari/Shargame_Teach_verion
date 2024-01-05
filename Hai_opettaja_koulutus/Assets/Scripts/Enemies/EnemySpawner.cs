using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;

    private Transform playerTransform;
    private float nextSpawnTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.IsGamePlay() || Time.time < nextSpawnTime)
        {
            return;
        }

        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if(playerTransform == null)
        {
            GetPlayer();
            return;
        }

        Vector2 spawnPos = playerTransform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        GameObject enemy = EnemyPoolManager.Instance.GetEnemy();
        enemy.transform.position = spawnPos;


        nextSpawnTime = Time.time + spawnInterval;
    }

    private void GetPlayer()
    {
        playerTransform = GameManager.Instance.playerController.transform;
        
    }
}
