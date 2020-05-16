using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Health : MonoBehaviour
{
    public event System.Action Died;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if(IsAlive)
            {
                if(value < 0) value = 0;
                else if(value > maxHealth) value = maxHealth;

                currentHealth = value;
                if(currentHealth == 0) Died?.Invoke();
                if(healthFillArea != null) healthFillArea.fillAmount = currentHealth / maxHealth;
                if(healthText != null) healthText.text = $"{Mathf.Ceil(HealthPercentage)}%";
            }
        }
    }
    public bool IsAlive => currentHealth > 0;
    public float HealthPercentage => currentHealth / maxHealth * 100;
    [Range(1, 1000)] [SerializeField] private float maxHealth = 100;
    [Range(0, 1000)] [SerializeField] private float currentHealth = 100;
    [SerializeField] private Image healthFillArea = null;
    [SerializeField] private TextMeshProUGUI healthText = null;

    private void Start()
    {
        CurrentHealth = currentHealth; // Update UI and death check
    }

    public void TakeDamage(float damage) 
    {
        CurrentHealth -= damage;
    }
}