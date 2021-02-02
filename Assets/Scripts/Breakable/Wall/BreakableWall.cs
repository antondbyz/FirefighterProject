using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private BrokenWall brokenWall = null;
    [SerializeField] private ParticleSystem wallBrokeEffect = null;
    
    private Transform childFire;

    private void Awake() 
    {
        childFire = GetComponentInChildren<Fire>(true)?.transform;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) Break();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.collider.CompareTag("Player")) Break();    
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("Extinguisher")) Break();    
    }

    private void Break()
    {
        ParticleSystem newEffect = Instantiate(wallBrokeEffect, transform.position, Quaternion.identity);
        GameController.DestroyWithDelay(newEffect.gameObject, 2);
        BrokenWall newBrokenWall = Instantiate(brokenWall, transform.position, Quaternion.identity);
        if(childFire != null)
        {
            childFire.SetParent(newBrokenWall.transform, true);
            newBrokenWall.ChildFire = childFire;
        }
        Destroy(gameObject);
    }
}