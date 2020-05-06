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
    }

    public void OpenTheMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}