using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesSpawner : MonoBehaviour
{
    public GameObject[] obstaclesPrefabs;
    public int startDelay;
    public int SpawnInterval;

    private PlayerController playerControllerScript;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacles", startDelay, SpawnInterval);
    }

    public void SpawnObstacles()
    {
        if (!playerControllerScript.gameOver)
        {
            int randomNum = Random.Range(0, obstaclesPrefabs.Length);
            GameObject obstacle = Instantiate(obstaclesPrefabs[randomNum]);
            obstacle.transform.position = transform.position;
        }
    }
}
