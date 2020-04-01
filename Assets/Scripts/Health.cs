using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public const float MAX_HEALTH = 100;
    public UnityEvent OnDie = new UnityEvent();
    public float CurrentHealth
    {
        get => currentHealth;
        private set
        {
            if(IsAlive)
            {
                if(value < 0) value = 0;
                else if(value > MAX_HEALTH) value = MAX_HEALTH;
                if(currentHealth != value)
                {
                    currentHealth = value;
                    if(currentHealth == 0) OnDie.Invoke();
                    if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / MAX_HEALTH;
                    if(healthValueText != null) healthValueText.text = $"{Mathf.Ceil(HealthPercentage)}";
                }
            }
        }
    }
    public bool IsAlive => currentHealth > 0;
    public float HealthPercentage => currentHealth / MAX_HEALTH * 100;
    [Range(0, MAX_HEALTH)] [SerializeField] private float currentHealth = MAX_HEALTH;
    [Range(1, 100)] [SerializeField] private float damageDefense = 1;
    [SerializeField] private Image healthFillArea = null;
    [SerializeField] private Text healthValueText = null;

    private void Start()
    {
        if(currentHealth == 0) OnDie.Invoke();
        if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / MAX_HEALTH;
        if(healthValueText != null) healthValueText.text = $"{Mathf.Ceil(HealthPercentage)}";
    }

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
    }
}