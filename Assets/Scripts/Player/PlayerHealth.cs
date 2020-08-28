using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
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

    private float lifesLeft = 3;
    private Player player;
    private GameController gameController;

    private void Awake()
    {
        player = GetComponent<Player>();
        gameController = GameObject.FindObjectOfType<GameController>();
        LifesLeft = lifesLeft;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Spikes")) Die();
    }

    private void Die()
    {
        LifesLeft--;
        if(lifesLeft > 0) player.MoveToLastSavedPosition();
        else gameController.FailLevel();
    }
}