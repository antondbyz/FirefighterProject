using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public void SetGamePause(bool value)
    {
        PauseManager.IsPaused = value;
    }

    public void RestartCurrentScene()
    {
        SceneManager.LoadScene(gameObject.scene.buildIndex);
        PauseManager.IsPaused = false;
    }

    public void OpenTheMainMenu()
    {
        SceneManager.LoadScene(0);
        PauseManager.IsPaused = false;
    }
}