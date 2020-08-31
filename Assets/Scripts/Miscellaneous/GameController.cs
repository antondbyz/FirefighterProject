using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour 
{
    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private UnityEvent levelPaused = null;

    private GameObject playerCharacter;
    private PlayerHealth playerHealth;
    private Player player;

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
        playerCharacter = GameObject.FindWithTag("Player").transform.GetChild(0).gameObject;
        playerHealth = playerCharacter.GetComponent<PlayerHealth>();  
        player = playerCharacter.GetComponent<Player>(); 
    }

    private void OnEnable() => playerHealth.Died += PlayerDied;

    private void OnDisable() => playerHealth.Died -= PlayerDied;

    private void PlayerDied()
    {
        playerCharacter.SetActive(false);
        StartCoroutine(MovePlayerToCurrentCheckpoint());
    }

    private IEnumerator MovePlayerToCurrentCheckpoint()
    {
        yield return new WaitForSeconds(2);
        player.MoveToCurrentCheckpoint();
        playerCharacter.SetActive(true);
    }
}