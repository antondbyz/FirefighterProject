using UnityEngine;

public class BrokenObject : MonoBehaviour 
{
    public Transform LinkedFire { get; set; }

    [SerializeField] private float lifetime = 1;
    [SerializeField] private bool destroyOnCollision = false;

    private void Awake() 
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(destroyOnCollision)
        {
            if(LinkedFire != null) LinkedFire.SetParent(null, true); 
            Destroy(gameObject);
        }
    }
}