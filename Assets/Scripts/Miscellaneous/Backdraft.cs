using System.Collections;
using UnityEngine;

public class Backdraft : MonoBehaviour
{
    [SerializeField] private BreakableObject breakableToListen = null;
    [SerializeField] private ParticleSystem smoke = null;
    [SerializeField] private float delayBeforeExplosion = 1;
    [SerializeField] private float duration = 2;

    private ParticleSystem ps;
    private Collider2D coll;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();
        coll = GetComponent<Collider2D>();
        coll.enabled = false;
    }

    private void OnEnable() => breakableToListen.Broken += Explode;

    private void OnDisable() => breakableToListen.Broken -= Explode;

    private void Explode()
    {
        StartCoroutine(Exploding());
    }

    private IEnumerator Exploding()
    {
        smoke.Play();
        yield return new WaitForSeconds(delayBeforeExplosion);
        smoke.Stop();
        ps.Play();
        coll.enabled = true;
        yield return new WaitForSeconds(duration);
        ps.Stop();
        coll.enabled = false;
        Destroy(gameObject, 2);
    }
}