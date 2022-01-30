using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public static GameController Instance;
    
    public event System.Action GamePaused;
    public event System.Action GameUnpaused;
    public bool IsPaused
    {
        get => isPaused;
        set
        {
            if(isPaused != value)
            {
                if(value) GamePaused?.Invoke();
                else GameUnpaused?.Invoke();
            }
            isPaused = value;
            Time.timeScale = isPaused ? 0 : 1;
            gameUI.SetActive(!isPaused);
        }
    }
    public Player Player => player;
    
    public UnityEvent LevelFailed;
    public UnityEvent LevelCompleted;

    [SerializeField] private Player player = null;
    [SerializeField] private GameObject gameUI = null;
    [SerializeField] private Transform starsContainer = null;
    [SerializeField] private TMP_Text totalVictimsSavedText = null;
    [SerializeField] private TMP_Text totalFiresExtinguishedText = null;
    [SerializeField] private TMP_Text totalMoneyEarnedText = null;
    [SerializeField] private Image gameBackground = null;
    [SerializeField] private ExtraLivesController extraLives = null;

    private bool isPaused;
    private Image[] stars; 
    private WaitForSeconds delayAfterDeath = new WaitForSeconds(1);

    public void CompleteLevel()
    {
        player.gameObject.SetActive(false);
        IsPaused = true;
        totalVictimsSavedText.text = $"{player.VictimsSaved}/{player.VictimsAmount}";
        totalFiresExtinguishedText.text = $"{player.FiresExtinguished}/{player.FiresAmount}";
        totalMoneyEarnedText.text = $"+{player.EarnedMoney.ToString()}";
        float levelCompletionCoefficient = (float)(player.VictimsSaved + player.FiresExtinguished) / (player.VictimsAmount + player.FiresAmount);
        int starsAmount = Mathf.RoundToInt(levelCompletionCoefficient * Level.MAX_STARS);
        for(int i = 0; i < starsAmount; i++) stars[i].color = Color.yellow;
        GameManager.FinishLevel(gameObject.scene.buildIndex, starsAmount, player.EarnedMoney);
        LevelCompleted.Invoke();
    } 

    public void FailLevel() => LevelFailed.Invoke();

    public void CloseLevelWithAd()
    {
        bool success = AdsManager.Instance.ShowInterstitialAd();
        if(success)
            AdsManager.Instance.InterstitialClosed += ScenesManager.Instance.ToTheMainMenu;
        else 
            ScenesManager.Instance.ToTheMainMenu();
    }
    
    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of GameController!");
        
        IsPaused = false;
        stars = new Image[starsContainer.childCount];
        for(int i = 0; i < stars.Length; i++) stars[i] = starsContainer.GetChild(i).GetComponent<Image>();
        Sprite[] gameBackgrounds = Resources.LoadAll<Sprite>("GameBackgrounds");
        gameBackground.sprite = gameBackgrounds[Random.Range(0, gameBackgrounds.Length)];
    }

    private void OnEnable() 
    {
        player.Died += PlayerDied;
    }

    private void OnDisable() 
    { 
        player.Died -= PlayerDied;
        AdsManager.Instance.InterstitialClosed -= ScenesManager.Instance.ToTheMainMenu;
        IsPaused = false;
    }

    private void PlayerDied()
    {
        player.gameObject.SetActive(false);
        if(player.LifesLeft > 0) StartCoroutine(MovePlayerToCurrentCheckpoint());
        else StartCoroutine(TryFailLevel());
    }

    private IEnumerator TryFailLevel()
    {
        yield return delayAfterDeath;
        IsPaused = true;
        if(!extraLives.IsPlayerRewarded) extraLives.ShowExtraLivesPanel();
        else FailLevel();
    }

    private IEnumerator MovePlayerToCurrentCheckpoint()
    {
        yield return delayAfterDeath;
        player.MoveToCurrentCheckpoint();
        player.gameObject.SetActive(true);
    }
}