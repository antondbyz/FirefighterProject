using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private BrokenWall brokenVersion = null;
    [SerializeField] private ParticleSystem wallBrokeEffect = null;

    private Transform childFire;

    private void Awake() 
    {
        childFire = GetComponentInChildren<Fire>().transform;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) Break();    
    }

    private void Break()
    {
        ParticleSystem newEffect = Instantiate(wallBrokeEffect, transform.position, Quaternion.identity);
        GameController.DestroyWithDelay(newEffect.gameObject, 2);
        BrokenWall newBrokenWall = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        if(childFire != null)
        {
            childFire.SetParent(newBrokenWall.transform, true);
            newBrokenWall.ChildFire = childFire;
        }
        Destroy(gameObject);
    }
}