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
    private PlayerLifes playerLifes;
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
        playerLifes = playerCharacter.GetComponent<PlayerLifes>();  
        player = playerCharacter.GetComponent<Player>(); 
    }

    private void OnEnable() => playerLifes.Died += PlayerDied;

    private void OnDisable() => playerLifes.Died -= PlayerDied;

    private void PlayerDied()
    {
        playerCharacter.SetActive(false);
        StartCoroutine(MovePlayerToCurrentCheckpoint());
    }

    private IEnumerator MovePlayerToCurrentCheckpoint()
    {
        yield return new WaitForSeconds(1);
        if(playerLifes.LifesLeft > 0)
        {
            player.MoveToCurrentCheckpoint();
            playerCharacter.SetActive(true);
        }
        else FailLevel();
    }
}