using System.Collections;
using UnityEngine;

public class FireHealth : Damageable
{
    [SerializeField] private Fire fire;
    private float receivingDamage;
    private ParticleSystem fireEffect;
    private BoxCollider2D boxCollider;
    private WaitForSeconds wait = new WaitForSeconds(0.1f);

    protected override void Awake()
    {
        base.Awake();
        fireEffect = GetComponent<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void StartExtinguishing(ExtinguishingSubstance substance)
    {
        receivingDamage += substance.Damage;
        StartCoroutine(ToFadeOut());
    }

    public void StopExtinguishing(ExtinguishingSubstance substance)
    {
        receivingDamage -= substance.Damage;
    }

    private IEnumerator ToFadeOut()
    {
        while(receivingDamage > 0)
        {
            TakeDamage(receivingDamage);
            yield return wait;
        }
    }

    public override void Die(float destroyDelay = 2)
    {
        Destroy(fire.SparksSpawner);
        Destroy(gameObject, destroyDelay);
        boxCollider.enabled = false;
        fireEffect.Stop();
    }
}