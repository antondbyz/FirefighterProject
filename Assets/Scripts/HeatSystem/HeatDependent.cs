using UnityEngine;

public abstract class HeatDependent : MonoBehaviour 
{
    public event System.Action HeatChanged;
    public float MaxHeat => maxHeat;
    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            if(value > MaxHeat) value = MaxHeat;
            else if(value < 0) value = 0;
            if(currentHeat != value)
            {
                currentHeat = value;
                UpdateState();
                HeatChanged?.Invoke();
            }
        } 
    }

    [SerializeField] private float maxHeat = 100;
    [SerializeField] private float currentHeat = 0;

    public abstract void UpdateState();
}