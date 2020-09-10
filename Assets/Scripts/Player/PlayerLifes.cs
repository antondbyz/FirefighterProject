using TMPro;
using UnityEngine;

public class PlayerLifes : MonoBehaviour
{
    public event System.Action Died;
    public float LifesLeft
    {
        get => lifesLeft;
        private set
        {
            lifesLeft = value;
            lifesText.text = lifesLeft.ToString();
        }
    }

    [SerializeField] private TextMeshProUGUI lifesText = null;
    [SerializeField] private ParticleSystem deathEffect = null;

    private Transform myTransform;
    private float lifesLeft = 3;
    private bool canDie = true;

    public void Die()
    {
        if(canDie)
        {
            canDie = false;
            LifesLeft--;
            // deathEffect.Play();
            Died?.Invoke();
        }
    }

    private void Awake()
    {
        myTransform = transform;
        lifesText.text = lifesLeft.ToString();
    }

    private void OnEnable() => canDie = true;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("DeathZone")) Die();
    }
}