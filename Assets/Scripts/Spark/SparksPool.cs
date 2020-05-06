using UnityEngine;

public class SparksPool : ObjectPool<Spark>
{
    public void ThrowSpark(Vector2 direction, float sparkHeat) 
    {
        Spark spark = SpawnFromPool(transform.position);
        StartCoroutine(spark.Throw(direction, sparkHeat));
    }
}