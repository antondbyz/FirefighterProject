using UnityEngine;

public static class PauseManager
{
    public static bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            Time.timeScale = isPaused ? 0 : 1;
        }
    }

    private static bool isPaused;
}