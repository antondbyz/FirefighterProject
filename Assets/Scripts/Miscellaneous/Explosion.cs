using System.Collections;
using UnityEngine;

public class Explosion : MonoBehaviour 
{
    [SerializeField] private float delayBeforeExplosion = 1;
    [SerializeField] private float delayBeforeColliderEnabling = 0.3f;
    [SerializeField] private float duration = 1;
    [SerializeField] private float delayBeforeSelfDestroy = 1;
    [SerializeField] private BreakableObject breakable = null;
    [SerializeField] private ParticleSystem smoke = null;
    [SerializeField] private Fire[] hiddenFires = new Fire[0];

    private ParticleSystem explosion;
    private Collider2D myCollider;

    private void Awake() 
    {
        explosion = GetComponent<ParticleSystem>();
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
    }

    private void OnEnable() => breakable.Broken += StartExplosion;

    private void OnDisable() => breakable.Broken -= StartExplosion;

    private void StartExplosion() => StartCoroutine(Explode());

    private IEnumerator Explode()
    {
        for(int i = 0; i < hiddenFires.Length; i++) hiddenFires[i].gameObject.SetActive(true);
        smoke?.Stop();
        yield return new WaitForSeconds(delayBeforeExplosion);
        explosion.Play();
        yield return new WaitForSeconds(delayBeforeColliderEnabling);
        myCollider.enabled = true;
        yield return new WaitForSeconds(duration);
        myCollider.enabled = false;
        explosion.Stop();
        yield return new WaitForSeconds(delayBeforeSelfDestroy);
        Destroy(gameObject);
    }
}