using UnityEngine;

public class Heat : MonoBehaviour 
{
    public const float MAX_POSSIBLE_HEAT = 1000;
    public event System.Action HeatChanged;
    public float HeatRatio => CurrentHeat / MaxHeat;
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
                HeatChanged?.Invoke();
            }
        } 
    }

    [Range(1, MAX_POSSIBLE_HEAT)] [SerializeField] private float maxHeat = 100;
    [Range(0, MAX_POSSIBLE_HEAT)] [SerializeField] private float currentHeat = 0;

    private void Start() 
    {
        HeatChanged?.Invoke();    
    }
}