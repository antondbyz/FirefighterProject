using UnityEngine;
using TMPro;

public class PlayerLifes : MonoBehaviour 
{
    public event System.Action Died;
    public int LifesLeft
    {
        get => lifesLeft;
        private set
        {
            if(value > maxLifes) value = maxLifes;
            else if(value < 0) value = 0;
            lifesLeft = value;
            lifesText.text = "Lifes: " + lifesLeft;
        }
    }

    [SerializeField] private TextMeshProUGUI lifesText = null;
    [SerializeField] private int maxLifes = 3;

    private int lifesLeft;
    private bool canDie;

    public void Die()
    {
        if(canDie)
        {
            canDie = false;
            LifesLeft--;
            Died?.Invoke();
        }
    }

    private void Awake() => LifesLeft = maxLifes;

    private void OnEnable() => canDie = true;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }
}