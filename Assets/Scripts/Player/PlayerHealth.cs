using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public event System.Action Died;
    public float LifesLeft
    {
        get => lifesLeft;
        private set
        {
            lifesLeft = value;
            healthText.text = lifesLeft.ToString();
        }
    }

    [SerializeField] private TextMeshProUGUI healthText = null;
    [SerializeField] private ParticleSystem deathEffect = null;

    private Transform myTransform;
    private float lifesLeft = 3;

    public void Die()
    {
        LifesLeft--;
        // deathEffect.transform.position = myTransform.position;
        // deathEffect.Play();
        Died?.Invoke();
    }

    private void Awake()
    {
        myTransform = transform;
        healthText.text = lifesLeft.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("DeathZone")) Die();
    }
}