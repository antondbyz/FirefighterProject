using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private GameObject brokenVersion = null;
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
        GameObject newBrokenVersion = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        Rigidbody2D rb = newBrokenVersion.GetComponent<Rigidbody2D>();
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