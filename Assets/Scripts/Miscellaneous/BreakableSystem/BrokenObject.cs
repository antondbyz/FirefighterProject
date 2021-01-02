using UnityEngine;

public class BrokenObject : MonoBehaviour 
{
    [SerializeField] private float lifetime = 1;
    [SerializeField] private bool destroyOnCollision = false;

    private void Awake() 
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(destroyOnCollision) Destroy(gameObject);
    }
}