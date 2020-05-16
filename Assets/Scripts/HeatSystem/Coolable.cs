using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Coolable : MonoBehaviour 
{
    public bool IsCooling => heat.CurrentHeat > 0 && (burnable == null || !burnable.IsBurning);

    [Tooltip("0 - the object does not cool at all; 1 - the object cools immediately")]
    [Range(0, 1)] [SerializeField] private float coolingCoefficient = 0.01f;
    
    private Heat heat;
    private Burnable burnable;
    private Coroutine coolingCoroutine;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        burnable = GetComponent<Burnable>();    
    }

    private void OnEnable() 
    {
        heat.HeatChanged += CheckCooling;    
    }

    private void OnDisable() 
    {
        heat.HeatChanged -= CheckCooling;    
    }

    private void CheckCooling()
    {
        if(IsCooling && coolingCoroutine == null)
            coolingCoroutine =  StartCoroutine(CoolingDown());
    }

    private IEnumerator CoolingDown()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(IsCooling)
        {
            heat.CurrentHeat -= heat.MaxHeat * coolingCoefficient;
            yield return delay;
        }
        coolingCoroutine = null;
    }    
}