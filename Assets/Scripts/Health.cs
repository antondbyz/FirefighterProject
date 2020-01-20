using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public virtual float CurrentHealth 
    {
        get{ return currentHealth; }
        set
        {
            if(IsAlive)
            {
                currentHealth = value;
                if(currentHealth <= 0)
                {
                    currentHealth = 0;
                    onDie.Invoke();
                } 
                else if(currentHealth > MaxHealth) currentHealth = MaxHealth;
                if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / MaxHealth;
                if(healthText != null) healthText.text = $"{(int)HealthPercentage}%";
                OnHealthChanged.Invoke(HealthPercentage);
            }
        }
    }
    public bool IsAlive => currentHealth > 0;
    public float HealthPercentage => currentHealth.NumberInRangeToPercentage(0, MaxHealth);
    public float MaxHealth => maxHealth;
    public FloatEvent OnHealthChanged { get; protected set; } = new FloatEvent();
    
    [SerializeField] protected UnityEvent onDie = null;
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected Image healthFillArea = null;
    [SerializeField] protected Text healthText = null;

    protected virtual void Awake()
    {
        CurrentHealth = currentHealth; // Updating HealthFillArea, HealthText and death check
    }

    public virtual void Die(float destroyDelay)
    {
        Destroy(gameObject, destroyDelay);
    }
}