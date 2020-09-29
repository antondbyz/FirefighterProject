using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHeat : MonoBehaviour 
{
    public const float MAX_HEAT = 100;
    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            value = Mathf.Clamp(value, 0, MAX_HEAT);
            currentHeat = value;
            if(currentHeat > 0) burningEffect.Play();
            else burningEffect.Stop();
        } 
    }
    public float CurrentHeatProtection
    {
        get => currentHeatProtection;
        private set
        {
            value = Mathf.Clamp(value, 0, maxHeatProtection);
            currentHeatProtection = value;
            heatProtectionAmountFill.fillAmount = currentHeatProtection / maxHeatProtection;
            if(currentHeatProtection == 0) lifes.Die();
        }
    }

    [SerializeField] private float maxHeatProtection = 0;
    [SerializeField] private Image heatProtectionAmountFill = null;
    [SerializeField] private ParticleSystem burningEffect = null;

    private float currentHeat; 
    private float currentHeatProtection;
    private PlayerLifes lifes;

    private void Awake() 
    {
        lifes = GetComponent<PlayerLifes>();
    }

    private void OnEnable() 
    {
        CurrentHeat = 0; 
        CurrentHeatProtection = maxHeatProtection;
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