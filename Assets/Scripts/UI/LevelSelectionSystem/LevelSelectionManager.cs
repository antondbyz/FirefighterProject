using UnityEngine;

public class LevelSelectionManager : UI_Manager<LevelItem>
{
    public static int CurrentLevelIndex = 0;

    [SerializeField] private GameObject playButton = null;

    public void PlaySelectedLevel() 
    { 
        CurrentLevelIndex = selectedItemIndex;
        SceneLoader.ReplaceCurrentScene(SelectedItem.LevelBuildIndex);
    }

    protected override void Awake() 
    {
        base.Awake();
        int lastAvailableLevelIndex = 0;
        for(int i = 0; i < GameManager.Levels.Length; i++)
        {
            if(GameManager.Levels[i].IsCompleted) 
            {
                lastAvailableLevelIndex = i + 1;
                lastAvailableLevelIndex = Mathf.Clamp(lastAvailableLevelIndex, 0, GameManager.Levels.Length - 1);
            }
            LevelItem newItem = Instantiate(item, itemsParent);
            newItem.Initialize(i, GameManager.Levels[i].BuildIndex, GameManager.Levels[i].StarsAmount);
            items.Add(newItem);
        }
        UpdateItemsAvailability(lastAvailableLevelIndex);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        SelectItem(CurrentLevelIndex, false);  
    }

    protected override void SelectItem(int index, bool playSound)
    {
        base.SelectItem(index, playSound);
        playButton.SetActive(items[index].IsAvailable);
    }
}