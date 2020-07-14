using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Extinguisher extinguisher = null;

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
        ExtinguishingSubstance substance = other.GetComponent<ExtinguishingSubstance>();
        if(substance != null)
        {
            extinguisher.CurrentSubstanceAmount += substance.Amount;
            Destroy(substance.gameObject);
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