using UnityEngine;

public class LevelSelectionManager : UI_Manager<LevelItem>
{
    [SerializeField] private GameObject playButton = null;

    private static int currentLevelIndex = 0;

    public void PlaySelectedLevel() 
    { 
        currentLevelIndex = selectedItemIndex;
        GameManager.LoadScene(SelectedItem.LevelBuildIndex);
    }

    private void Awake() 
    {
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
        SelectItem(currentLevelIndex);  
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        playButton.SetActive(items[index].IsAvailable);
    }
}