using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public virtual bool IsAlive => currentHealth > 0;
    public float HealthPercentage => currentHealth.NumberInRangeToPercentage(0, MaxHealth);
    public virtual float CurrentHealth 
    {
        get{ return currentHealth; }
        protected set
        {
            if(IsAlive)
            {
                currentHealth = value;
                if(currentHealth <= 0)
                {
                    currentHealth = 0;
                    OnDie.Invoke();
                } 
                else if(currentHealth > MaxHealth) currentHealth = MaxHealth;
                if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / MaxHealth;
                if(healthText != null) healthText.text = $"{(int)HealthPercentage}%";
                OnHealthChanged.Invoke(HealthPercentage);
            }
        }
    }
    public virtual float MaxHealth => maxHealth;
    public FloatEvent OnHealthChanged { get; protected set; }
    
    [SerializeField] protected UnityEvent OnDie = null;
    [SerializeField] protected float maxHealth = 100;
    [SerializeField] protected float currentHealth = 100;
    [SerializeField] protected Image healthFillArea = null;
    [SerializeField] protected Text healthText = null;

    protected virtual void Awake()
    {
        OnHealthChanged = new FloatEvent();
        CurrentHealth = currentHealth; // Updating HealthFillArea, HealthText and death check
    }

    public virtual void TakeDamage(float value)
    {
        CurrentHealth -= value;
    }

    public virtual void ToTreat(float value)
    {
        CurrentHealth += value;
    }

    public virtual void SetHealth(float value)
    {
        CurrentHealth = value;
    }

    public virtual void Die(float destroyDelay)
    {
        Destroy(gameObject, destroyDelay);
    }
}