using UnityEngine;

public class BrokenWall : MonoBehaviour 
{
    public Transform ChildFire { get; set; }

    [SerializeField] private float lifetime = 1;
    [SerializeField] private ParticleSystem wallFellEffect = null;

    private Transform myTransform;

    private void Awake() 
    {
        myTransform = transform;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        ChildFire?.SetParent(null, true); 
        ParticleSystem newEffect = Instantiate(wallFellEffect, myTransform.position, Quaternion.identity);
        GameController.DestroyWithDelay(newEffect.gameObject, 2);
        Destroy(gameObject);
    }
}