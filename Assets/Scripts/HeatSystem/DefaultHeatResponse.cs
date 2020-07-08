using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class DefaultHeatResponse : MonoBehaviour, IHeatResponse
{
    public bool IsDamaging => health != null && heat.CurrentHeat > 0;
    public bool IsCooling => heat.CurrentHeat > 0 && (burnable == null || !burnable.IsBurning);

    private Heat heat;
    private SpriteRenderer spriteRenderer;
    private Health health;
    private Burnable burnable;
    private Coroutine coolingCoroutine;
    private Coroutine damagingCoroutine;
    private float latestHeat;

    public void Response()
    {
        if(spriteRenderer != null)
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, heat.CurrentHeat / heat.MaxHeat);
        
        if(IsDamaging && damagingCoroutine == null)
            damagingCoroutine = StartCoroutine(Damaging());

        if(heat.CurrentHeat > latestHeat && coolingCoroutine != null)
        {
            StopCoroutine(coolingCoroutine);
            coolingCoroutine = null;
        }
        if(IsCooling && coolingCoroutine == null)
            coolingCoroutine =  StartCoroutine(Cooling());
        
        latestHeat = heat.CurrentHeat;
    }

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = GetComponent<Health>();
        burnable = GetComponent<Burnable>();  
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

    private IEnumerator Cooling()
    {
        yield return new WaitForSeconds(1);
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {
            heat.CurrentHeat -= heat.MaxHeat / 100;
            yield return delay;
        }
    }
}