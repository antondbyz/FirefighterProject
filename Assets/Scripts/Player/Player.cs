using UnityEngine;

public class Player : MonoBehaviour
{
    private GameController gameController;
    private PlayerHealth health;
    private ExtinguishingSubstance extinguishingSubstance;

    public void PauseLevel() => gameController.PauseLevel();

    private void Awake() 
    {
        gameController = GameObject.FindObjectOfType<GameController>();    
        health = GetComponent<PlayerHealth>();
        extinguishingSubstance = transform.GetComponentInChildren<ExtinguishingSubstance>();
    }

    private void OnEnable() => health.Died += gameController.FailLevel;

    private void OnDisable() => health.Died -= gameController.FailLevel;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("ExtinguisherRefill"))
        {
            extinguishingSubstance.CurrentSubstanceAmount = ExtinguishingSubstance.MAX_SUBSTANCE_AMOUNT;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Finish")) gameController.CompleteLevel();
    }
}