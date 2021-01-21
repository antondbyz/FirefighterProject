using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Backdraft : MonoBehaviour
{
    [SerializeField] private BreakableObstacle obstacle = null;
    [SerializeField] private ParticleSystem backdraftSmoke = null;
    [SerializeField] private ParticleSystem smokeInRoom = null;
    [SerializeField] private ParticleSystem[] fireInRoom = null;
    [SerializeField] private float delayBeforeExplosion = 1;
    [SerializeField] private float duration = 2;

    private ParticleSystem backdraft;
    private Collider2D coll;

    private void Awake() 
    {
        backdraft = GetComponent<ParticleSystem>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
        for(int i = 0; i < fireInRoom.Length; i++) 
        {
            fireInRoom[i].gameObject.SetActive(false);
            MainModule main = fireInRoom[i].main;
            main.prewarm = false;
        }
    }

    private void OnEnable() => obstacle.Broke += Explode;

    private void OnDisable() => obstacle.Broke -= Explode;

    private void Explode() => StartCoroutine(Exploding());

    private IEnumerator Exploding()
    {
        backdraftSmoke.Play();
        yield return new WaitForSeconds(delayBeforeExplosion);
        for(int i = 0; i < fireInRoom.Length; i++) fireInRoom[i].gameObject.SetActive(true);
        backdraft.Play();
        coll.enabled = true;
        yield return new WaitForSeconds(duration);
        smokeInRoom.Stop();
        GameController.DestroyWithDelay(smokeInRoom.gameObject, smokeInRoom.main.startLifetimeMultiplier);
        backdraftSmoke.Stop();
        backdraft.Stop();
        coll.enabled = false;
        Destroy(gameObject, 2);
    }
}