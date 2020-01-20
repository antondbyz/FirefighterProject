using System.Collections;
using UnityEngine;

public class SparksSpawner : ObjectPooler<Spark>
{
    //[SerializeField] private Fire fire = null;
    private float frequency;
    private float yForce;
    private float xForce;

    // public IEnumerator SpawnSparks(Fire sparksSpawner)
    // {
    //     while(true)
    //     {
    //         yield return new WaitForSeconds(1 / frequency);
    //         Spark newSpark = SpawnFromPool(transform.position);
    //         newSpark.Initialize(sparksSpawner);
    //         Vector2 side = Random.Range(0, 1f) > 0.5f ? Vector2.right : Vector2.left;
    //         newSpark.AddForce(side * xForce);
    //         newSpark.AddForce(Vector2.up * yForce);
    //     }  
    // }

    public Spark[] SpawnAllSparksFromPool()
    {
        Spark[] spawnedSparks = new Spark[poolSize];
        for(int i = 0; i < poolSize; i++)
        {
            spawnedSparks[i] = SpawnFromPool(transform.position);
        }
        return spawnedSparks;
    }

    public void UpdateSparksSpawning(float newFrequency, float newYForce, float newXForce)
    {
        frequency = newFrequency;
        yForce = newYForce;
        xForce = newXForce;
    } 
}