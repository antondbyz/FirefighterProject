using UnityEngine;

public class BreakableWall : BackdraftObstacle
{
    [SerializeField] private Transform spikes = null;
    [SerializeField] private Pool brokenWallsPool = null;
    [SerializeField] private Pool wallBrokeEffectsPool = null;

    private Transform myTransform;

    private void Awake() 
    { 
        myTransform = transform;  
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) Break(other.transform.position.y < myTransform.position.y);    
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("Extinguisher")) Break(true);    
    }

    private void Break(bool dangerous)
    {
        BrokenWall newBrokenWall = brokenWallsPool.SpawnObject(myTransform.position).GetComponent<BrokenWall>(); 
        newBrokenWall.Initialize(spikes, dangerous);
        wallBrokeEffectsPool.SpawnObject(myTransform.position);
        Disappeared?.Invoke();
        Destroy(gameObject);
    }
}