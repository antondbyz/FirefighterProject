using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Extinguisher Extinguisher => extinguisher;

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private Extinguisher extinguisher = null;

    private Health health;

    private void Awake() 
    {
        PauseManager.IsPaused = false;
        health = GetComponent<Health>();
    }

    private void OnEnable() 
    {
        if(health != null)
            health.Died += FailLevel;
    }

    private void OnDisable() 
    {
        if(health != null)
            health.Died -= FailLevel;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Finish finish = other.GetComponent<Finish>();
        if(finish != null)
        {
            CompleteLevel();
        }        
    }

    public void FailLevel()
    {
        PauseManager.IsPaused = true;
        levelFailed.Invoke();
    }

    public void CompleteLevel()
    {
        PauseManager.IsPaused = true;
        levelCompleted.Invoke();
    }
}