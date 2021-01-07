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
        }
    }

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;

    private PlayerLifes playerLifes;
    private Player player;
    private bool isPaused;
    private WaitForSeconds delay = new WaitForSeconds(1);

    public static void DestroyWithDelay(GameObject go, float delay) => Destroy(go, delay);

    public void CompleteLevel()
    {
        player.gameObject.SetActive(false);
        IsPaused = true;
        levelCompleted.Invoke();
    } 
    
    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of GameController!");

        player = transform.GetChild(0).GetComponent<Player>();
        playerLifes = player.GetComponent<PlayerLifes>();
        IsPaused = false;
    }

    private void OnEnable() => playerLifes.Died += PlayerDied;

    private void OnDisable() => playerLifes.Died -= PlayerDied;

    private void PlayerDied()
    {
        player.gameObject.SetActive(false);
        if(playerLifes.LifesLeft > 0)
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