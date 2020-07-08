using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public event System.Action LevelFailed;
    public event System.Action LevelCompleted;

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;

    public void FailLevel()
    {
        PauseManager.IsPaused = true;
        levelFailed.Invoke();
        LevelFailed?.Invoke();
    }

    public void CompleteLevel()
    {
        PauseManager.IsPaused = true;
        levelCompleted.Invoke();
        LevelCompleted?.Invoke();
    }    
}