using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Extinguisher extinguisher = null;

    private Health health;
    private GameController gameController;
    private Wounded wounded;

    public void PauseLevel() => gameController.PauseLevel();

    private void Awake() 
    {
        health = GetComponent<Health>();
        gameController = GameObject.FindObjectOfType<GameController>();    
        wounded = GameObject.FindObjectOfType<Wounded>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        ExtinguishingSubstance substance = other.GetComponent<ExtinguishingSubstance>();
        if(substance != null)
        {
            extinguisher.CurrentSubstanceAmount += substance.Amount;
            Destroy(substance.gameObject);
        }
    }

    private void OnEnable() 
    {
        health.Died += gameController.FailLevel; 
        wounded.Recovered += gameController.CompleteLevel;   
    }

    private void OnDisable() 
    {
        health.Died -= gameController.FailLevel;    
        wounded.Recovered -= gameController.CompleteLevel;
    }
}