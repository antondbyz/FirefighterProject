using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class PlayerHeat : MonoBehaviour 
{
    [System.Serializable]
    private struct BurningEffectValues
    {
        public float Size;
        public float Gravity;
    }

    public const float MAX_HEAT = 100;
    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            if(value > MAX_HEAT) value = MAX_HEAT;
            else if(value < 0) value = 0;
            currentHeat = value;
            if(currentHeat > 0) 
            {
                burningEffect.Play();
                burningEffectMain.startSize = Mathf.Lerp(minBurningParticle.Size, maxBurningParticle.Size, currentHeat / MAX_HEAT);
                burningEffectMain.gravityModifier = Mathf.Lerp(minBurningParticle.Gravity, maxBurningParticle.Gravity, currentHeat / MAX_HEAT);
            }
            else burningEffect.Stop();
        } 
    }
    public float CurrentHeatProtection
    {
        get => currentHeatProtection;
        private set
        {
            if(value > heatProtection) value = heatProtection;
            else if(value < 0) value = 0;
            currentHeatProtection = value;
            heatProtectionAmountFill.fillAmount = currentHeatProtection / heatProtection;
            if(currentHeatProtection == 0) lifes.Die();
        }
    }

    [SerializeField] private float heatProtection = 0;
    [SerializeField] private Image heatProtectionAmountFill = null;
    [SerializeField] private ParticleSystem burningEffect = null;
    [SerializeField] private BurningEffectValues minBurningParticle = new BurningEffectValues();
    [SerializeField] private BurningEffectValues maxBurningParticle = new BurningEffectValues();

    private float currentHeat; 
    private float currentHeatProtection;
    private PlayerLifes lifes;
    private MainModule burningEffectMain;

    private void Awake() 
    {
        lifes = GetComponent<PlayerLifes>();
        burningEffectMain = burningEffect.main;
    }

    private void OnEnable()
    {
        CurrentHeat = 0; 
        CurrentHeatProtection = heatProtection;
        StartCoroutine(Damaging());
    }

    private IEnumerator Damaging()
    {
        float timeDelay = 0.1f;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            if(currentHeat > 0) CurrentHeatProtection -= currentHeat * timeDelay;
        }
    }
}