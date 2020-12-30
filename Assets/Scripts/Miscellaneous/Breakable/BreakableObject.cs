using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private Rigidbody2D brokenVersion = null;
    [SerializeField] private bool triggerCheck = false;
    [SerializeField] private bool collisionCheck = false;

    public void Break()
    {
        Instantiate(brokenVersion, transform.position, Quaternion.identity);
        Broken?.Invoke();
        Destroy(gameObject);
    }

    public void Break(Vector2 direction, float force)
    {
        Rigidbody2D rb = Instantiate<Rigidbody2D>(brokenVersion, transform.position, Quaternion.identity);
        rb.AddForce(direction * force);
        Broken?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(triggerCheck && other.CompareTag("Player")) Break();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(collisionCheck && other.gameObject.CompareTag("Player")) Break();    
    }
}