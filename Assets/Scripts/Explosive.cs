using UnityEngine;

public class Explosive : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionEffect = null;
    [SerializeField] private float explosionRadius = 5f;
    [SerializeField] private float force = 10f;
    [SerializeField] private SparksSpawner sparksSpawner = null;
    [SerializeField] private LayerMask collidesWith = new LayerMask();

    public void ToExplode()
    {
        explosionEffect?.Play();

        Spark[] spawnedSparks = sparksSpawner.SpawnAllSparksFromPool();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, collidesWith);
        for(int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].TryGetComponent(out Rigidbody2D rb))
            {
                Vector2 side = colliders[i].transform.position - transform.position;
                rb.AddTorque(-side.x * force);
                rb.AddForce(side * force);
            }
        }
    }    
}