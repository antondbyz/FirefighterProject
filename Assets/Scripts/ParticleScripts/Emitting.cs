using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Emitting : MonoBehaviour
{
    public bool IsEmitting { get; private set; }

    [SerializeField] protected float maxEmission = 30, minEmission = 0;
    protected EmissionModule emission;
    private WaitForSeconds dyingTime = new WaitForSeconds(1.5f);

    protected virtual void Awake()
    {
        emission = GetComponent<ParticleSystem>().emission;
    }

    public virtual void StartEmit()
    {
        IsEmitting = true;
        emission.rateOverTime = maxEmission;
    }
    public virtual void StopEmit()
    {
        IsEmitting = false;
        emission.rateOverTime = minEmission;
    }

    public virtual void Die()
    {
        StartCoroutine(Dying());
    }

    private IEnumerator Dying()
    {
        yield return dyingTime;
        Destroy(gameObject);
    }
}