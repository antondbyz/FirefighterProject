using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public const int FIRE_EXTINGUISHED_REWARD = 10;
    public const int VICTIM_SAVED_REWARD = 100;

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
    public static PlayerSkin CurrentPlayerSkin;
    public static int LastCompletedLevelIndex;

    private static int playerBalance;

    public static void LevelCompleted()
    {
        PlayerBalance = GameController.Instance.NewPlayerBalance;
        LastCompletedLevelIndex =  GameController.Instance.gameObject.scene.buildIndex;
    }

    public static void LoadScene(int loadScene)
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(loadScene, LoadSceneMode.Additive);
    }

    private void Awake() 
    {
        PlayerSkins = Resources.LoadAll<PlayerSkin>("PlayerSkins");    
        CurrentPlayerSkin = PlayerSkins[0];
        SceneManager.sceneLoaded += SceneLoaded;
        if(SceneManager.sceneCount == 1) SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }   

    private void OnEnable() => GameController.LevelCompleted += LevelCompleted;

    private void OnDisable() => GameController.LevelCompleted -= LevelCompleted;

    private void SceneLoaded(Scene scene, LoadSceneMode mode) => SceneManager.SetActiveScene(scene);
}