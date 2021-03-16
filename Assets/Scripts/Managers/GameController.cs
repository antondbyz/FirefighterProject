using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public static GameController Instance;
    public static event System.Action<int> LevelCompleted;

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
    public int NewPlayerBalance => player.EarnedMoney;

    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    [SerializeField] private Player player = null;
    [SerializeField] private GameObject gameUI = null;
    [SerializeField] private Transform starsContainer = null;
    
    private bool isPaused;
    private GameObject[] victims;
    private Image[] stars; 
    private WaitForSeconds delay = new WaitForSeconds(1);

    public void CompleteLevel()
    {
        player.gameObject.SetActive(false);
        IsPaused = true;
        int victimsSaved = 0;
        for(int i = 0; i < victims.Length; i++)
        {
            if(victims[i] == null) victimsSaved++;
        }
        float victimsSavedCoefficient = (float)victimsSaved / victims.Length;
        int starsAmount = Mathf.RoundToInt((float)victimsSaved / victims.Length * Level.MAX_STARS);
        for(int i = 0; i < starsAmount; i++) stars[i].color = Color.yellow;
        LevelCompleted?.Invoke(starsAmount);
        levelCompleted.Invoke();
    } 
    
    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of GameController!");
        IsPaused = false;
        victims = GameObject.FindGameObjectsWithTag("Victim");
        stars = new Image[starsContainer.childCount];
        for(int i = 0; i < stars.Length; i++) stars[i] = starsContainer.GetChild(i).GetComponent<Image>();
    }

    private void OnEnable() => player.Died += PlayerDied;

    private void OnDisable() => player.Died -= PlayerDied;

    private void PlayerDied()
    {
        player.gameObject.SetActive(false);
        if(player.LifesLeft > 0) StartCoroutine(MoveCharacterToCurrentCheckpoint());
        else StartCoroutine(FailLevel());
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