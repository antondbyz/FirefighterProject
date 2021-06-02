using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public const int FIRE_EXTINGUISHED_REWARD = 10;
    public const int VICTIM_SAVED_REWARD = 50;
    public static event System.Action PlayerBalanceChanged;
    public static int PlayerBalance 
    {
        get => playerBalance;
        set
        {
            if(value < 0) value = 0;
            if(playerBalance != value)
            {
                playerBalance = value;
                PlayerBalanceChanged?.Invoke();
            }
        }
    }
    public static PlayerSkin[] PlayerSkins;
    public static Level[] Levels;

    private static int playerBalance;

    [SerializeField] private int firstLevelBuildIndex = 3;

    public static void FinishLevel(int finishedLevelBuildIndex, int starsAmount, int earnedMoney)
    {
        PlayerBalance += earnedMoney;
        for(int i = 0; i < Levels.Length; i++)
        {
            if(Levels[i].BuildIndex < finishedLevelBuildIndex) Levels[i].Complete();
            else if(Levels[i].BuildIndex == finishedLevelBuildIndex)
            {
                Levels[i].Complete();
                Levels[i].ChangeStarsAmount(starsAmount);
            }
            else break;
        }
    }

    public void InitializeLevels()
    {
        Levels = new Level[SceneManager.sceneCountInBuildSettings - firstLevelBuildIndex];
        for(int i = 0; i < Levels.Length; i++) Levels[i] = new Level(i + firstLevelBuildIndex);
    }

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of GameManager!");
        
        PlayerSkins = Resources.LoadAll<PlayerSkin>("PlayerSkins");
        if(Levels == null) InitializeLevels();
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => 
        {
            SceneManager.SetActiveScene(scene);
            if(scene.buildIndex >= firstLevelBuildIndex) SaveManager.SaveGame();
        };
        SceneManager.sceneUnloaded += (Scene scene) =>
        {
            if(scene.buildIndex >= firstLevelBuildIndex) SaveManager.SaveGame();
        };
        if(SceneManager.sceneCount == 1) SceneLoader.LoadNewScene(1);
    }
}