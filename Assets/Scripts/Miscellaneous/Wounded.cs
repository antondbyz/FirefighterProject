using System.Collections;
using UnityEngine;

public class Wounded : MonoBehaviour
{
    public float CurrentHealth 
    { 
        get => currentHealth; 
        set
        {
            if(value > maxHealth) value = maxHealth;
            else if(value < 0) value = 0;
            currentHealth = value;
            if(currentHealth < maxHealth) animator.SetBool("Wounded", true);
            else 
            {
                animator.SetBool("Wounded", false);
                StartCoroutine(DestroyAfterDelay(new WaitForSeconds(5)));
            }
        } 
    }
    public float MaxHealth => maxHealth;

    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 0;

    private Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        CurrentHealth = currentHealth;    
    }

    private IEnumerator DestroyAfterDelay(WaitForSeconds delay)
    {
        yield return delay;
        Destroy(gameObject);
    }
}