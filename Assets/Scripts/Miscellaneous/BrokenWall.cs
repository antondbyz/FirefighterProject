using System.Collections;
using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private float lifetime = 1;
    [SerializeField] private GameObject deathZone = null;
    [SerializeField] private Pool wallFellEffectsPool = null;

    private Transform myTransform;
    private Transform spikes;
    private Coroutine liveCoroutine;
    private WaitForSeconds disableDelay;

    public void Initialize(Transform newSpikes, bool dangerous) 
    {
        spikes = newSpikes;
        if(spikes != null) spikes.SetParent(myTransform, true);
        deathZone.SetActive(dangerous);
        if(liveCoroutine != null) StopCoroutine(liveCoroutine);
        liveCoroutine = StartCoroutine(Live());
    }

    private void Awake() 
    {
        myTransform = transform;
        disableDelay = new WaitForSeconds(lifetime);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(spikes != null) 
        {
            spikes.gameObject.SetActive(true);
            spikes.SetParent(null, true);
        }
        wallFellEffectsPool.SpawnObject(myTransform.position);
        gameObject.SetActive(false);
    }

    private IEnumerator Live()
    {
        yield return disableDelay;
        gameObject.SetActive(false);
    }
}