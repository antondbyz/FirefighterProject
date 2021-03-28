using System.Collections;
using UnityEngine;

public class Backdraft : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeStream = null;
    [SerializeField] private ParticleSystem smokeInRoom = null;
    [SerializeField] private AudioSource backdraftAudio = null;
    [SerializeField] private float delayBeforeExplosion = 1;
    [SerializeField] private float duration = 2;
    [SerializeField] private Obstacle obstacle = null;
    [SerializeField] private ParticleSystem[] fireInRoom = null;

    private ParticleSystem backdraft;
    private Coroutine explosionCoroutine;

    private void Awake() 
    {
        backdraft = GetComponent<ParticleSystem>();
        smokeInRoom.Play();
        for(int i = 0; i < fireInRoom.Length; i++) 
        {
            fireInRoom[i].gameObject.SetActive(false);
            ParticleSystem.MainModule main = fireInRoom[i].main;
            main.prewarm = false;
        }
    }

    private void OnEnable() => obstacle.Disappeared += StartExplosion;

    private void OnDisable() => obstacle.Disappeared -= StartExplosion;
    
    private void StartExplosion() 
    {
        if(explosionCoroutine == null) 
           explosionCoroutine = StartCoroutine(Explode());
    } 

    public IEnumerator Explode()
    {
        smokeStream.Play();
        smokeInRoom.Stop();
        yield return new WaitForSeconds(delayBeforeExplosion);
        for(int i = 0; i < fireInRoom.Length; i++) fireInRoom[i].gameObject.SetActive(true);
        backdraft.Play(false);
        backdraftAudio.Play();
        yield return new WaitForSeconds(duration);
        backdraft.Stop(false);
        smokeStream.Stop();
        backdraftAudio.Stop();
    }
}