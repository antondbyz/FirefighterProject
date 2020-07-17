using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ExtinguishingSubstance extinguishingSubstance = null;

    private Health health;
    private GameController gameController;

    public void PauseLevel() => gameController.PauseLevel();

    private void Awake() 
    {
        health = GetComponent<Health>();
        gameController = GameObject.FindObjectOfType<GameController>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("ExtinguisherRefill"))
        {
            extinguishingSubstance.CurrentSubstanceAmount = ExtinguishingSubstance.MAX_SUBSTANCE_AMOUNT;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Finish")) gameController.CompleteLevel();
    }

    private void OnEnable() 
    {
        health.Died += gameController.FailLevel;  
    }

    private void OnDisable() 
    {
        health.Died -= gameController.FailLevel;    
    }
}