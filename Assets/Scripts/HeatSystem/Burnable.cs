using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Heatable))]
public class Burnable : MonoBehaviour 
{
    public bool IsBurning => heatable.CurrentHeat >= heatForBurning;

    [SerializeField] private ParticleSystem burningParticles = null;
    [SerializeField] private float heatForBurning = 10;
    [Tooltip("0 - the object does not heat at all, 1 - the object heats immediately")]
    [Range(0, 1)] [SerializeField] private float heatingCoefficient = 0.02f;

    private Heatable heatable;
    private Coroutine heatingCoroutine;
    private IScalable particlesScaler;

    private void Awake() 
    {
        heatable = GetComponent<Heatable>();
        particlesScaler = burningParticles.GetComponent<IScalable>();
        CheckBurning();
    }

    private void OnEnable() 
    {
        heatable.HeatChanged += CheckBurning;    
    }

    private void OnDisable() 
    {
        heatable.HeatChanged -= CheckBurning;    
    }

    private void CheckBurning()
    {
        if(IsBurning)
        {
            burningParticles.Play();
            if(particlesScaler != null) 
            {
                float scale;
                if(heatForBurning == heatable.MaxHeat) scale = 1;
                else scale = (heatable.CurrentHeat - heatForBurning) / (heatable.MaxHeat - heatForBurning);
                particlesScaler.LerpScale(heatable.CurrentHeat / heatable.MaxHeat);
            }
            if(heatingCoefficient > 0 && heatingCoroutine == null) 
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
            heatable.CurrentHeat += heatable.MaxHeat * heatingCoefficient;
            yield return delay;
        }
        heatingCoroutine = null;
    }
}