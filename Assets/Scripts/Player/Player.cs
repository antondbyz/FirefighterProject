using UnityEngine;

public class Player : MonoBehaviour
{
    public Extinguisher Extinguisher => extinguisher;

    [SerializeField] private Extinguisher extinguisher = null;
 
    private Health health;
    private GameController gameController;

    private void Awake() 
    {
        PauseManager.IsPaused = false;
        health = GetComponent<Health>();
        gameController = GameObject.FindObjectOfType<GameController>();
    } 

    private void OnEnable() 
    {
        if(health != null)
            health.Died += gameController.FailLevel;
    }

    private void OnDisable() 
    {
        if(health != null)
            health.Died -= gameController.FailLevel;    
    }
}