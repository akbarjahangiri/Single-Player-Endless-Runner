using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Road : MonoBehaviour
{

    public ObjectPool<GameObject> pool;
    public List<GameObject> obstacles = new List<GameObject>();
    private GameObject previousObstacle;


    private void Start()
    {
        pool = FindObjectOfType<SpawnManager>().pool;
    }

    void Update()
    {
        transform.position += Vector3.back * Time.deltaTime * GameManager.Instance.movementSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            ObstacleRandomize();
            pool.Get();
            pool.Release(gameObject);
        }
    }

    public void ObstacleRandomize()
    {
        if (previousObstacle)
        {
            previousObstacle.gameObject.SetActive(false);
        }

        previousObstacle = obstacles[Random.Range(0, obstacles.Count)];
        previousObstacle.SetActive(true);
    }
}