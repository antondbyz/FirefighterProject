using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Extinguisher Extinguisher => extinguisher;

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private Extinguisher extinguisher = null;

    private Health health;
    private Wounded wounded;

    private void Awake() 
    {
        PauseManager.IsPaused = false; 
        health = GetComponent<Health>();
        wounded = GameObject.FindObjectOfType<Wounded>();
    }

    private void FailLevel()
    {
        PauseManager.IsPaused = true;
        levelFailed.Invoke();
    }

    private void CompleteLevel()
    {
        PauseManager.IsPaused = true;
        levelCompleted.Invoke();
    }

    private void OnEnable() 
    {
        health.Died += FailLevel;    
        if(wounded != null) wounded.Recovered += CompleteLevel;
    }

    private void OnDisable() 
    {
        health.Died -= FailLevel; 
        if(wounded != null) wounded.Recovered -= CompleteLevel;   
    } 
}