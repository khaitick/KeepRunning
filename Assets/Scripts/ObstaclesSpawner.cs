using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject obstaclesPrefab;
    public int startDelay;
    public int SpawnInterval;

    private void Start()
    {
        InvokeRepeating("SpawnObstacles", startDelay, SpawnInterval);
    }

    public void SpawnObstacles()
    {
        Instantiate(obstaclesPrefab);
    }
}
