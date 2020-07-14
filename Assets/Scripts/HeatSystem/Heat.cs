using UnityEngine;

public class Heat : MonoBehaviour
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
                if(!IsHeatable && value > currentHeat) return;
                currentHeat = value;
                HeatChanged?.Invoke();
            }
        } 
    }
    public bool IsHeatable => isHeatable;
    public bool IsExtinguishable => isExtinguishable;

    [SerializeField] private float maxHeat = 100;
    [SerializeField] private float currentHeat = 0;
    [SerializeField] private bool isHeatable = true;
    [SerializeField] private bool isExtinguishable = true;

    private void Start() 
    {
        HeatChanged?.Invoke();   
    }
}