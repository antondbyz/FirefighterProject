using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectsPool<T> : MonoBehaviour where T: Component
{
    public int PoolSize => poolSize;
    [SerializeField] protected T prefab = null;
    [SerializeField] protected int poolSize = 1;
    protected Queue<T> objectsPool = new Queue<T>();

    protected void Start()
    {
        for(int i = 0; i < poolSize; i++)
        {
            T obj = Instantiate(prefab);
            obj.gameObject.SetActive(false);
            objectsPool.Enqueue(obj);
        }
    }

    protected T SpawnFromPool(Vector2 position)
    {
        T objectToSpawn = objectsPool.Dequeue();
        objectToSpawn.gameObject.SetActive(true);
        objectToSpawn.transform.position = position;
        objectsPool.Enqueue(objectToSpawn);
        return objectToSpawn;
    }

    private void OnDestroy() 
    {
        foreach(T obj in objectsPool)
        {
            if(obj != null) Destroy(obj.gameObject);
        }    
    }
}