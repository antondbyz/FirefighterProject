using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private GameController gameController;

    private void Awake()
    {
        gameController = GameObject.FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Spikes")) Die();
    }

    private void Die()
    {
        gameController.FailLevel();
    }
}