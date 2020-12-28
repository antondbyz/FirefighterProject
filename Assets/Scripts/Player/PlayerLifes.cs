using UnityEngine;
using TMPro;

public class PlayerLifes : MonoBehaviour 
{
    public const int MAX_LIFES = 3;

    public event System.Action Died;
    public int LifesLeft
    {
        get => lifesLeft;
        private set
        {
            value = Mathf.Clamp(value, 0, MAX_LIFES);
            lifesLeft = value;
            lifesText.text = "Lifes: " + lifesLeft;
        }
    }

    [SerializeField] private TextMeshProUGUI lifesText = null;

    private int lifesLeft;
    private float currentHealth;

    public void Die()
    {
        if(gameObject.activeSelf)
        {
            LifesLeft--;
            Died?.Invoke();
        }
    }

    private void Awake() 
    {
        LifesLeft = MAX_LIFES;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("DeathZone")) Die();    
    }
}