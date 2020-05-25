using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class DefaultHeatResponse : MonoBehaviour, IHeatResponse
{
    public bool IsDamaging => health != null && heat.CurrentHeat > 0;

    private Heat heat;
    private SpriteRenderer spriteRenderer;
    private Health health;
    private Coroutine damagingCoroutine;

    public void Response()
    {
        if(spriteRenderer != null)
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, heat.CurrentHeat / heat.MaxHeat);
        
        if(IsDamaging && damagingCoroutine == null)
            damagingCoroutine = StartCoroutine(Damaging());
    }

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
    }

    private IEnumerator Damaging()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(IsDamaging)
        {
            health.CurrentHealth -= heat.CurrentHeat / heat.MaxHeat;
            yield return delay;
        }
        damagingCoroutine = null;
    }
}