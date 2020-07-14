using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Burnable : MonoBehaviour 
{
    public bool IsBurning => heat.CurrentHeat >= HeatForBurning;
    public float HeatForBurning => heat.MaxHeat / 10;

    [SerializeField] private ParticleSystem burningEffect = null;

    private Heat heat;
    private ParticlesScaler burningEffectScaler;
    private Coroutine heatingCoroutine;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        burningEffectScaler = burningEffect.GetComponent<ParticlesScaler>();
    }

    private void Start() 
    {
        CheckBurning();    
    }

    private void OnEnable() => heat.HeatChanged += CheckBurning;    

    private void OnDisable() => heat.HeatChanged -= CheckBurning;    

    private void CheckBurning()
    {
        if(IsBurning)
        {
            burningEffect.Play();
            burningEffectScaler?.Scale(heat.CurrentHeat / heat.MaxHeat);
            if(heatingCoroutine == null) 
                heatingCoroutine = StartCoroutine(HeatingUp());
        }
        else burningEffect.Stop();
    }

    private IEnumerator HeatingUp()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(IsBurning)
        {
            yield return delay;
            heat.CurrentHeat++;
        }
        heatingCoroutine = null;
    }
}