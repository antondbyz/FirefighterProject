using UnityEngine;

public static class PauseManager
{
    public static event System.Action OnPaused;
    public static event System.Action OnResumed;
    public static bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            if(isPaused)
            {
                Time.timeScale = 0;
                OnPaused?.Invoke();
            }
            else 
            {
                Time.timeScale = 1;
                OnResumed?.Invoke();
            }
        }
    }

    private static bool isPaused;
}