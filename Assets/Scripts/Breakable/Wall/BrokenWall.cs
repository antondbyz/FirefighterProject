using UnityEngine;

public class BrokenWall : MonoBehaviour 
{
    [HideInInspector] public bool CreateSpikesWhenFell;

    [SerializeField] private float lifetime = 1;
    [SerializeField] private GameObject spikes = null;
    [SerializeField] private ParticleSystem wallFellEffect = null;

    private Transform myTransform;

    private void Awake() 
    {
        myTransform = transform;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(CreateSpikesWhenFell) Instantiate(spikes, myTransform.position, Quaternion.identity);
        ParticleSystem newEffect = Instantiate(wallFellEffect, myTransform.position, Quaternion.identity);
        GameController.DestroyWithDelay(newEffect.gameObject, 2);
        Destroy(gameObject);
    }
}