using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour
{
    public Asteroid asteroidPrefab;
    private float spawnRate = 1f;
    private float timeStart = 1f;
    private float spawnDistance = 12f;
    public float trajectoryVariance = 15f;

    private void Start()
    {
        InvokeRepeating("Spawn", timeStart, spawnRate);
    }
    public void Spawn()
    {
        Vector3 spawnDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = transform.position + (spawnDirection * spawnDistance);

        float variace = Random.Range(-trajectoryVariance, trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variace, Vector3.forward);

        Asteroid asteroid =Instantiate(asteroidPrefab, spawnPoint, rotation);
        asteroid.size = Random.Range(asteroid.minSize, asteroid.maxSize);

        Vector2 trajectory = rotation * -spawnDirection;
        asteroid.SetTrajectory(trajectory);
    }
}
