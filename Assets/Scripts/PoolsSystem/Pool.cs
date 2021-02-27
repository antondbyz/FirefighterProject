using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="My assets/New pool")]
public class Pool : ScriptableObject
{
    public GameObject Prefab;
    public int Size;
    public Queue<GameObject> Objects;

    public GameObject SpawnObject(Vector2 position)
    {
        GameObject spawnedObj = Objects.Dequeue();
        spawnedObj.SetActive(true);
        spawnedObj.transform.position = position;
        Objects.Enqueue(spawnedObj);
        return spawnedObj;
    }
}