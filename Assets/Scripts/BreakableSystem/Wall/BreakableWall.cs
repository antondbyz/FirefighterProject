using UnityEngine;

public class BreakableWall : Breakable
{
    [SerializeField] private bool createSpikesWhenFell = true;
    [SerializeField] private BrokenWall brokenWall = null;
    [SerializeField] private BrokenWall brokenWallDangerous = null;
    [SerializeField] private Pool wallBrokeEffectsPool = null;

    private Transform myTransform;
    private BrokenWall newBrokenWall;

    private void Awake() => myTransform = transform;    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(other.transform.position.y > myTransform.position.y)
                newBrokenWall = Instantiate(brokenWall, myTransform.position, Quaternion.identity); 
            else
                newBrokenWall = Instantiate(brokenWallDangerous, myTransform.position, Quaternion.identity);
            Break();    
        }
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("Extinguisher")) 
        {
            newBrokenWall = Instantiate(brokenWallDangerous, myTransform.position, Quaternion.identity);
            Break();    
        }
    }

    private void Break()
    {
        newBrokenWall.CreateSpikesWhenFell = createSpikesWhenFell;
        wallBrokeEffectsPool.SpawnObject(myTransform.position);
        InvokeBroke();
        Destroy(gameObject);
    }
}