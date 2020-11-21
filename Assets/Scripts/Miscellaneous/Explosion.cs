using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour 
{
    [SerializeField] private float force = 300;
    [SerializeField] private float startExplosionDelay = 1;
    [SerializeField] private float deathZoneActivatingDelay = 0.3f;
    [SerializeField] private float stopExplosionDelay = 1;
    [SerializeField] private float selfDestroyDelay = 1;
    [SerializeField] private BreakableObject breakableToListen = null;
    [SerializeField] private BreakableObject breakableToBreak = null;
    [SerializeField] private Fire[] hiddenFires = new Fire[0];

    private ParticleSystem explosion;
    private GameObject deathZone;
    private Coroutine explosionCoroutine;

    private void Awake() 
    {
        explosion = GetComponent<ParticleSystem>();
        deathZone = transform.GetChild(0).gameObject;
    }

    private void OnEnable() 
    { 
        if(breakableToListen != null) breakableToListen.Broken += StartExplosion;
    }

    private void OnDisable() 
    { 
        if(breakableToListen != null) breakableToListen.Broken -= StartExplosion;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player")) StartExplosion();
    }

    private void StartExplosion() 
    { 
        if(explosionCoroutine == null) explosionCoroutine = StartCoroutine(Explode());
    }

    private IEnumerator Explode()
    {
        for(int i = 0; i < hiddenFires.Length; i++) hiddenFires[i].gameObject.SetActive(true);
        yield return new WaitForSeconds(startExplosionDelay);
        explosion.Play();
        breakableToBreak.Break(transform.right, force);
        yield return new WaitForSeconds(deathZoneActivatingDelay);
        deathZone.SetActive(true);
        yield return new WaitForSeconds(stopExplosionDelay);
        deathZone.SetActive(false);
        explosion.Stop();
        yield return new WaitForSeconds(selfDestroyDelay);
        Destroy(gameObject);
    }
}