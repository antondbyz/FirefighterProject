using System.Collections;
using UnityEngine;

public class Heatable : HeatDependent
{
    public bool IsDamaging => health != null && CurrentHeat > 0;
    public bool IsCooling => CurrentHeat > 0 && (burnable == null || !burnable.IsBurning);

    private SpriteRenderer spriteRenderer;
    private Health health;
    private Burnable burnable;
    private Coroutine damagingCoroutine;
    private Coroutine coolingCoroutine;
    private float latestHeat;

    public override void UpdateState()
    {
        if(spriteRenderer != null)
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, CurrentHeat / MaxHeat);
        
        if(IsDamaging && damagingCoroutine == null)
            damagingCoroutine = StartCoroutine(Damaging());

        if(CurrentHeat > latestHeat)
        {
            if(coolingCoroutine != null)
            {
                StopCoroutine(coolingCoroutine);
                coolingCoroutine = null;
            }
        }

        if(IsCooling && coolingCoroutine == null)
            coolingCoroutine =  StartCoroutine(CoolingDown());
        
        latestHeat = CurrentHeat;
    }

    private void Awake() 
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        health = GetComponent<Health>();
        burnable = GetComponent<Burnable>();
        UpdateState();
    }

    private IEnumerator Damaging()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(IsDamaging)
        {
            health.CurrentHealth -= CurrentHeat / MaxHeat;
            yield return delay;
        }
        damagingCoroutine = null;
    }

    private IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(1);
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {
            CurrentHeat -= MaxHeat / 100;
            yield return delay;
        }
    }  
}