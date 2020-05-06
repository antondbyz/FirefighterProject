using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SparksPool sparksPool;
    private ParticleSystem explosionEffect;
    private GameObject parent;
    private Health parentHealth;

    private void Awake() 
    {
        sparksPool = transform.GetChild(0).GetComponent<SparksPool>();
        explosionEffect = GetComponent<ParticleSystem>();
        parent = transform.parent.gameObject;
        parentHealth = parent.GetComponent<Health>();
        parentHealth.Died += Explode;
    }

    public void Explode()
    {
        transform.parent = null;
        Destroy(parent);
        explosionEffect.Play();
        for(int i = 0; i < sparksPool.PoolSize; i++)
        {
            sparksPool.ThrowSpark(new Vector2(Random.Range(-150, 150), Random.Range(100, 350)), Random.Range(1, 10));
        }
        Destroy(gameObject, 3);
    }   

    private void OnDisable() 
    {
        parentHealth.Died -= Explode;
    } 
}