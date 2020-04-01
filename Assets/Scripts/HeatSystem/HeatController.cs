using System.Collections;
using UnityEngine;

public class HeatController : Heat
{
    public const float HEAT_FOR_BURNING = MAX_HEAT / 10;
    [Range(1, 100)] [SerializeField] private float heatResistance = 1;
    [SerializeField] private BurningEffect burningEffect = null;
    private Health health;
    private SpriteRenderer rend;
    private Coroutine coolingCoroutine, heatingCoroutine, takingDamageCoroutine;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);
    private WaitForSeconds delayBeforeCooling = new WaitForSeconds(3);

    private void Awake() 
    {
        health = GetComponent<Health>();
        rend = GetComponent<SpriteRenderer>();
        OnHeatChanged += UpdateState;    
    }

    public override void ToHeat(float heat)
    {
        if(heat > 0)
        {
            heat /= heatResistance;
            if(coolingCoroutine != null)
            {
                StopCoroutine(coolingCoroutine);
                coolingCoroutine = null;
            }
        }
        CurrentHeat += heat;
    }

    private void UpdateState()
    {
        rend.color = Color.Lerp(Color.white, Color.red, CurrentHeat / MAX_HEAT);
        if(health != null && takingDamageCoroutine == null) takingDamageCoroutine = StartCoroutine(TakingDamage());
        if(burningEffect != null && CurrentHeat >= HEAT_FOR_BURNING)
        {
            if(heatingCoroutine == null) heatingCoroutine = StartCoroutine(Heating());
            if(coolingCoroutine != null)
            {
                StopCoroutine(coolingCoroutine);
                coolingCoroutine = null;
            } 
        }
        else if(coolingCoroutine == null) coolingCoroutine = StartCoroutine(Cooling());
    }

    private IEnumerator Cooling()
    {
        yield return delayBeforeCooling;
        while(true)
        {   
            yield return delay;
            CurrentHeat--;
        }
    }

    private IEnumerator Heating()
    {
        while(true)
        {
            yield return delay;
            if(CurrentHeat >= HEAT_FOR_BURNING) CurrentHeat++;
        }
    }

    private IEnumerator TakingDamage()
    {
        while(true)
        {
            yield return delay;
            health.TakeDamage(CurrentHeat);
        }
    }
}