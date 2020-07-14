using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public event System.Action Died;
    public float CurrentHealth
    {
        get => currentHealth;
        set
        {
            if(currentHealth > 0)
            {
                if(value < 0) value = 0;
                else if(value > maxHealth) value = maxHealth;

                currentHealth = value;
                if(currentHealth == 0) Died?.Invoke();
                if(healthFill != null) healthFill.fillAmount = currentHealth / maxHealth;
            }
        }
    }
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    [SerializeField] private Image healthFill = null;

    private void Start()
    {
        CurrentHealth = currentHealth;
    }
}