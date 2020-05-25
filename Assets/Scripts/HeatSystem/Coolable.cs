using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Coolable : MonoBehaviour
{
    public bool IsCooling => heat.CurrentHeat > 0 && (burnable == null || !burnable.IsBurning);

    private Heat heat;
    private Burnable burnable;
    private Coroutine coolingCoroutine;
    private float latestHeat;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        burnable = GetComponent<Burnable>();   
    }

    private void Start() 
    {
        UpdateCooling();    
    }

    private void OnEnable() 
    {
        heat.HeatChanged += UpdateCooling;    
    }

    private void OnDisable() 
    {
        heat.HeatChanged -= UpdateCooling;    
    }

    private void UpdateCooling()
    {
        if(heat.CurrentHeat > latestHeat && coolingCoroutine != null)
        {
            StopCoroutine(coolingCoroutine);
            coolingCoroutine = null;
        }

        if(IsCooling && coolingCoroutine == null)
            coolingCoroutine =  StartCoroutine(CoolingDown());
        
        latestHeat = heat.CurrentHeat;
    }

    private IEnumerator CoolingDown()
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