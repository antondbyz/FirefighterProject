using UnityEngine;

public class SparksPool : ObjectsPool<Spark>
{
    public void ThrowSpark(Vector2 direction, float sparkHeat) 
    {
        Spark spark = SpawnFromPool(transform.position);
        StartCoroutine(spark.Throw(direction, sparkHeat));
    }
}