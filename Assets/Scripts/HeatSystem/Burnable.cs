using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Burnable : MonoBehaviour 
{
    public bool IsBurning => heat.CurrentHeat >= HeatForBurning;
    public float HeatForBurning => heat.MaxHeat * heatCoefficientForBurning;

    [SerializeField] private ParticleSystem burningParticles = null;
    [Range(0, 1)] [SerializeField] private float heatCoefficientForBurning = 0.1f;
    [Tooltip("0 - the object does not heat at all, 1 - the object heats immediately")]
    [Range(0, 1)] [SerializeField] private float heatingCoefficient = 0.02f;

    private Heat heat;
    private Coroutine heatingCoroutine;
    private IScalable burningEffectScaler;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        burningEffectScaler = burningParticles.GetComponent<IScalable>();
    }

    private void OnEnable() 
    {
        heat.HeatChanged += UpdateBurning;    
    }

    private void OnDisable() 
    {
        heat.HeatChanged -= UpdateBurning;    
    }

    private void UpdateBurning()
    {
        if(IsBurning)
        {
            if(heatingCoroutine == null) heatingCoroutine = StartCoroutine(HeatingUp());
            if(!burningParticles.isPlaying) burningParticles.Play();
            if(burningEffectScaler != null)
            {
                float scale;
                if(HeatForBurning >= heat.MaxHeat) scale = 1;
                else scale = (heat.CurrentHeat - HeatForBurning) / (heat.MaxHeat - HeatForBurning);
                burningEffectScaler.LerpScale(scale);
            }
        }
        else if(burningParticles.isPlaying) burningParticles.Stop();
    }

    private IEnumerator HeatingUp()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(IsBurning)
        {
            heat.CurrentHeat += heat.MaxHeat * heatingCoefficient;
            yield return delay;
        }
        heatingCoroutine = null;
    }
}