using UnityEngine;

public abstract class Heat : MonoBehaviour
{
    public const float MAX_HEAT = 100;
    public event System.Action OnHeatChanged;
    public float CurrentHeat 
    {
        get => currentHeat;
        protected set
        {
            if(value > MAX_HEAT) value = MAX_HEAT;
            else if(value < 0) value = 0;
            if(currentHeat != value)
            {
                currentHeat = value;
                OnHeatChanged?.Invoke();
            }
        }
    }
    [Range(0, MAX_HEAT)] [SerializeField] private float currentHeat = 0;

    private void Start() 
    {
        OnHeatChanged?.Invoke();    
    }

    public virtual void ToHeat(float heat) 
    {
        CurrentHeat += heat; 
    }
}