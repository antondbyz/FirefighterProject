using System.Collections;
using UnityEngine;

public class Backdraft : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeStream = null;
    [SerializeField] private ParticleSystem smokeInRoom = null;
    [SerializeField] private float delayBeforeExplosion = 1;
    [SerializeField] private float duration = 2;
    [SerializeField] private BreakableObstacle obstacle = null;
    [SerializeField] private ParticleSystem[] fireInRoom = null;

    private ParticleSystem backdraft;

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
    
    private void OnEnable() => obstacle.Broke += StartExplosion;

    private void OnDisable() => obstacle.Broke -= StartExplosion;

    private void StartExplosion() => StartCoroutine(Explode());

    private IEnumerator Explode()
    {
        smokeStream.Play();
        smokeInRoom.Stop();
        yield return new WaitForSeconds(delayBeforeExplosion);
        for(int i = 0; i < fireInRoom.Length; i++) fireInRoom[i].gameObject.SetActive(true);
        backdraft.Play(false);
        yield return new WaitForSeconds(duration);
        backdraft.Stop();
    }
}