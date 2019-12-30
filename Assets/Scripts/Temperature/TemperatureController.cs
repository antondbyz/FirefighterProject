using System.Collections;
using UnityEngine;

public class TemperatureController : MonoBehaviour
{
    public bool debug = false;
    public bool IsDynamic = false;
    public float CurrentTemperature
    {
        get { return currentTemperature; }
        private set
        {
            onTemperatureChanged.Invoke(value - currentTemperature);
            currentTemperature = value;
        }
    }
    public TemperatureData TemperatureData => temperatureData;
    public FloatEvent OnTemperatureChanged => onTemperatureChanged;

    [SerializeField] private TemperatureData temperatureData = null;
    [SerializeField] private float currentTemperature = 0f;
    [SerializeField] private FloatEvent onTemperatureChanged = null;

    private float temperatureDelta = 0;
    private int temperatureEffectorsNumber = 0;
    private WaitForSeconds delay = new WaitForSeconds(0.5f);

    private void Start() 
    {
        if(IsDynamic)
        {
            onTemperatureChanged = new FloatEvent();
            StartCoroutine(BalanceTemperature()); 
        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(IsDynamic && other.TryGetComponent(out TemperatureController temperature))
        {
            temperatureDelta *= temperatureEffectorsNumber;
            temperatureEffectorsNumber++;
            temperatureDelta = (temperatureDelta + temperature.CurrentTemperature - currentTemperature) / temperatureEffectorsNumber;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(IsDynamic && other.TryGetComponent(out TemperatureController temperature))
        {
            temperatureDelta *= temperatureEffectorsNumber;
            temperatureEffectorsNumber--;
            if(temperatureEffectorsNumber != 0)
                temperatureDelta = (temperatureDelta - (temperature.CurrentTemperature - currentTemperature)) / temperatureEffectorsNumber;
        }    
    }

    private IEnumerator BalanceTemperature()
    {
        while(true)
        {
            yield return delay;
            if(temperatureDelta != 0)
            {
                float delta = Mathf.MoveTowards(temperatureDelta, 0, 1);
                CurrentTemperature += temperatureDelta - delta;
                temperatureDelta = delta;
                if(debug) Debug.Log($"Temperature {CurrentTemperature} Delta {temperatureDelta}");
            }
        }
    }
}