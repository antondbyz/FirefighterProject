using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [HideInInspector] public bool CreateSpikesWhenFell;

    [SerializeField] private float lifetime = 1;
    [SerializeField] private GameObject spikes = null;
    [SerializeField] private Pool wallFellEffectsPool = null;

    private Transform myTransform;

    private void Awake() 
    {
        myTransform = transform;
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(CreateSpikesWhenFell) 
        {
            spikes.SetActive(true);
            spikes.transform.SetParent(null, true);
        }
        wallFellEffectsPool.SpawnObject(myTransform.position);
        Destroy(gameObject);
    }
}