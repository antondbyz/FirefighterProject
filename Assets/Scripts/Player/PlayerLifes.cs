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
        lifesText.text = lifesLeft.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("DeathZone")) Die();
    }
}