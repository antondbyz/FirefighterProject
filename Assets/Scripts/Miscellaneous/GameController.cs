using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private UnityEvent levelPaused = null;

    public void SetGamePause(bool value) => PauseManager.IsPaused = value;

    public void RestartCurrentScene() => SceneManager.LoadScene(gameObject.scene.buildIndex);

    public void OpenTheMainMenu() => SceneManager.LoadScene(0);

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

    public void PauseLevel()
    {
        PauseManager.IsPaused = true;
        levelPaused.Invoke();
    }

    private void Awake() 
    {
        PauseManager.IsPaused = false;   
    }
}