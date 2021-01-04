using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private BrokenObject brokenVersion = null;
    [SerializeField] private Transform linkedFire = null;
    [SerializeField] private bool triggerCheck = false;
    [SerializeField] private bool collisionCheck = false;

    public void Break()
    {
        BrokenObject newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        if(linkedFire != null)
        {
            linkedFire.SetParent(newBrokenObj.transform, true);
            newBrokenObj.LinkedFire = linkedFire;
        }
        Broken?.Invoke();
        Destroy(gameObject);
    }

    public void Break(Vector2 force)
    {
        BrokenObject newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        newBrokenObj.GetComponent<Rigidbody2D>().AddForce(force);
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