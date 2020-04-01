using UnityEngine;

public class GameController : MonoBehaviour
{
    public event System.Action OnPaused;
    public bool IsPaused { get; private set; } = false;

    public void SetPause(bool value)
    {
        if(value)
        {
            Time.timeScale = 0;
            OnPaused?.Invoke();
        }
        else Time.timeScale = 1;
        IsPaused = value;
    }

    public void ChangeScene(int index)
    {
        SetPause(false);
        ScenesManager.ChangeScene(index);
    }

    public void RestartCurrentScene()
    {
        ChangeScene(ScenesManager.CurrentSceneBuildIndex);
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }
}