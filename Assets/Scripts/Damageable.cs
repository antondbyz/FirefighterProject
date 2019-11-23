using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Damageable : MonoBehaviour
{
    public float HealthPercentage => currentHealth.NumberToPercentage(0, maxHealth);
    public float CurrentHealth 
    {
        get{ return currentHealth; }
        private set
        {
            if(currentHealth > value) OnTakeDamage.Invoke();
            else if(currentHealth < value) OnTreat.Invoke();
            currentHealth = value;
            if(currentHealth <= 0)
            {
                currentHealth = 0;
                OnDie.Invoke();
            } 
            else if(currentHealth > maxHealth) currentHealth = maxHealth;
            if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / maxHealth;
            if(healthText != null) healthText.text = $"{(int)(currentHealth / (maxHealth / 100))}%";
            OnHealthChanged.Invoke(HealthPercentage);
        }
    }
    public float MaxHealth => maxHealth;

    public UnityEvent OnDie;
    public UnityEvent OnTakeDamage;
    public UnityEvent OnTreat;
    public FloatEvent OnHealthChanged { get; private set; }

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private Image healthFillArea = null;
    [SerializeField] private Text healthText = null;

    private void Awake()
    {
        OnHealthChanged = new FloatEvent();
        CurrentHealth = currentHealth; // Updating HealthFillArea, HealthText and death check
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    public void ToTreat(float value)
    {
        CurrentHealth += value;
    }

    public void SetHealth(float value)
    {
        CurrentHealth = value;
    }
}