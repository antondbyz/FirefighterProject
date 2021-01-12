using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private BrokenObject brokenVersion = null;
    [SerializeField] private Transform linkedFire = null;
    [SerializeField] private ParticleSystem breakingEffect = null;

    public void Break(Vector2 force)
    {
        BrokenObject newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        newBrokenObj.GetComponent<Rigidbody2D>().AddForce(force);
        Broken?.Invoke();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) Break();    
    }

    private void Break()
    {
        if(breakingEffect != null) 
        {
            ParticleSystem newEffect = Instantiate(breakingEffect, transform.position, Quaternion.identity);
            GameController.DestroyWithDelay(newEffect.gameObject, 2);
        }
        BrokenObject newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        if(linkedFire != null)
        {
            linkedFire.SetParent(newBrokenObj.transform, true);
            newBrokenObj.LinkedFire = linkedFire;
        }
        Broken?.Invoke();
        Destroy(gameObject);
    }
}