using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : UI_Manager<LevelItem>
{
    private static Level[] levels = null;
    private static int currentLevelIndex = 0;

    [SerializeField] private int firstLevelBuildIndex = 2;
    [SerializeField] private LevelItem item = null;
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private GameObject playButton = null;

    private int lastAvailableLevelIndex = 0;

    public static void CurrentLevelCompleted(int starsAmount)
    {
        levels[currentLevelIndex].Complete();
        levels[currentLevelIndex].ChangeStarsAmount(starsAmount);
    }

    public void PlaySelectedLevel() 
    {
        currentLevelIndex = SelectedItem.Index;
        GameManager.LoadScene(SelectedItem.LevelBuildIndex);
    }

    private void Awake() 
    {
        if(levels == null)
        { 
            levels = new Level[SceneManager.sceneCountInBuildSettings - firstLevelBuildIndex];
            for(int i = 0; i < levels.Length; i++)
            {
                levels[i] = new Level(i + firstLevelBuildIndex);
            }
        }
        for(int i = 0; i < levels.Length; i++)
        {
            if(levels[i].IsCompleted) 
            {
                lastAvailableLevelIndex = i + 1;
                lastAvailableLevelIndex = Mathf.Clamp(lastAvailableLevelIndex, 0, levels.Length - 1);
            }
            LevelItem newItem = Instantiate(item, itemsList);
            newItem.Initialize(i, levels[i].BuildIndex, levels[i].StarsAmount);
            items.Add(newItem);
        }
        UpdateItemsAvailability(lastAvailableLevelIndex);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        SelectItem(lastAvailableLevelIndex);  
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        playButton.SetActive(items[index].IsAvailable);
    }
}