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
    public static PlayerSkin CurrentPlayerSkin => PlayerSkins[ShopManager.UsingItemIndex];
    public static PlayerSkin[] PlayerSkins;
    public static Level[] Levels;

    private static int playerBalance;

    [SerializeField] private int firstLevelBuildIndex = 3;

    public static void LevelCompleted(int starsAmount, int earnedMoney)
    {
        PlayerBalance += earnedMoney;
        int completedLevelBuildIndex = GameController.Instance.gameObject.scene.buildIndex;
        for(int i = 0; i < Levels.Length; i++)
        {
            if(Levels[i].BuildIndex < completedLevelBuildIndex) Levels[i].Complete();
            else if(Levels[i].BuildIndex == completedLevelBuildIndex)
            {
                Levels[i].Complete();
                Levels[i].ChangeStarsAmount(starsAmount);
            }
            else break;
        }
    }

    private void Awake() 
    {
        PlayerSkins = Resources.LoadAll<PlayerSkin>("PlayerSkins");

        if(Levels == null)
        {
            Levels = new Level[SceneManager.sceneCountInBuildSettings - firstLevelBuildIndex];
            for(int i = 0; i < Levels.Length; i++) Levels[i] = new Level(i + firstLevelBuildIndex);
        }

        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => SceneManager.SetActiveScene(scene);
        if(SceneManager.sceneCount == 1) SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}