using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour 
{
    public static GameController Instance;

    public bool IsPaused
    {
        get => isPaused;
        set
        {
            isPaused = value;
            Time.timeScale = isPaused ? 0 : 1;
            gameUI.SetActive(!isPaused);
        }
    }

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private Player player = null;
    [SerializeField] private GameObject gameUI = null;
    
    private bool isPaused;
    private WaitForSeconds delay = new WaitForSeconds(1);

    public void CompleteLevel()
    {
        GameManager.PlayerBalance = player.EarnedMoney;
        player.gameObject.SetActive(false);
        IsPaused = true;
        levelCompleted.Invoke();
    } 
    
    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of GameController!");

        IsPaused = false;
    }

    private void OnEnable() => player.Died += PlayerDied;

    private void OnDisable() => player.Died -= PlayerDied;

    private void PlayerDied()
    {
        player.gameObject.SetActive(false);
        if(player.LifesLeft > 0)
        {
            StartCoroutine(MoveCharacterToCurrentCheckpoint());
        }
        else
        { 
            StartCoroutine(FailLevel());
        }
    }

    private IEnumerator FailLevel()
    {
        yield return delay;
        IsPaused = true;
        levelFailed.Invoke();
    }

    private IEnumerator MoveCharacterToCurrentCheckpoint()
    {
        yield return delay;
        player.MoveToCurrentCheckpoint();
        player.gameObject.SetActive(true);
    }   
}