using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public const float MAX_HEALTH = 100;
    public event System.Action Died;
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
                    UpdateState();
                }
            }
        }
    }
    public bool IsAlive => currentHealth > 0;
    public float HealthPercentage => currentHealth / MAX_HEALTH * 100;
    [Range(0, MAX_HEALTH)] [SerializeField] private float currentHealth = MAX_HEALTH;
    [Range(1, 100)] [SerializeField] private float damageDefense = 1;
    [SerializeField] private Image healthFillArea = null;
    [SerializeField] private TextMeshProUGUI healthText = null;

    private void Start()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if(currentHealth == 0) Died?.Invoke();
        if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / MAX_HEALTH;
        if(healthText != null) healthText.text = $"{Mathf.Ceil(HealthPercentage)}%";
    }

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage / damageDefense;
    }
}