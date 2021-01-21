using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private enum BreakEvent { TRIGGER_ENTER, COLLISION_ENTER }

    [SerializeField] private BreakEvent breakEvent = BreakEvent.TRIGGER_ENTER;
    [SerializeField] private BrokenWall brokenVersion = null;
    [SerializeField] private ParticleSystem wallBrokeEffect = null;
    
    private Transform childFire;

    private void Awake() 
    {
        childFire = GetComponentInChildren<Fire>(true)?.transform;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(breakEvent == BreakEvent.TRIGGER_ENTER && other.CompareTag("Player")) Break();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(breakEvent == BreakEvent.COLLISION_ENTER && other.collider.CompareTag("Player")) Break();    
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