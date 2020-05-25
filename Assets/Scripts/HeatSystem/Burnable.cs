using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Burnable : MonoBehaviour 
{
    public bool IsBurning => heat.CurrentHeat >= heatForBurning;

    [SerializeField] private ParticleSystem burningParticles = null;
    [SerializeField] private float heatForBurning = 10;
    [Tooltip("0 - the object does not heat at all, 1 - the object heats immediately")]
    [Range(0, 1)] [SerializeField] private float heatingCoefficient = 0.02f;

    private Heat heat;
    private ParticlesScaler particlesScaler;
    private Coroutine heatingCoroutine;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        particlesScaler = burningParticles.GetComponent<ParticlesScaler>();
    }

    private void Start() 
    {
        CheckBurning();    
    }

    private void OnEnable() 
    {
        heat.HeatChanged += CheckBurning;    
    }

    private void OnDisable() 
    {
        heat.HeatChanged -= CheckBurning;    
    }

    private void CheckBurning()
    {
        if(IsBurning)
        {
            burningParticles.Play();
            if(particlesScaler != null) 
            {
                float scale;
                if(heatForBurning == heat.MaxHeat) scale = 1;
                else scale = (heat.CurrentHeat - heatForBurning) / (heat.MaxHeat - heatForBurning);
                particlesScaler.LerpScale(heat.CurrentHeat / heat.MaxHeat);
            }
            if(heatingCoroutine == null) 
                heatingCoroutine = StartCoroutine(HeatingUp());
        }
        else burningParticles.Stop();
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