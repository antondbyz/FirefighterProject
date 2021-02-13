using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private Pool[] pools = null;

    private Dictionary<Pool, Queue<GameObject>> poolsDictionary = new Dictionary<Pool, Queue<GameObject>>();
    private Transform myTransform;

    private void Awake() 
    {
        if(Instance != null) Destroy(gameObject);
        Instance = this;
        myTransform = transform;
        for(int i = 0; i < pools.Length; i++)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>(); 
            for(int j = 0; j < pools[i].Size; j++)
            {
                GameObject obj = Instantiate(pools[i].Prefab, myTransform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolsDictionary.Add(pools[i], objectPool);
        }    
    }

    public GameObject SpawnObject(Pool pool, Vector2 position)
    {
        GameObject spawnedObj = poolsDictionary[pool].Dequeue();
        spawnedObj.SetActive(true);
        spawnedObj.transform.position = position;
        poolsDictionary[pool].Enqueue(spawnedObj);
        return spawnedObj;
    }
}