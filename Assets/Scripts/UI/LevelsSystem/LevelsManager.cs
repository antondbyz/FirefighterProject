using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : UI_Manager<LevelItem>
{
    [SerializeField] private int firstLevelBuildIndex = 2;
    [SerializeField] private LevelItem item = null;
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private GameObject playButton = null;

    private static int lastAvailableLevelIndex;

    public void PlaySelectedLevel() => GameManager.LoadScene(selectedItem.LevelBuildIndex);

    private void Awake() 
    {
        if(GameManager.LastCompletedLevelIndex >= firstLevelBuildIndex)
            lastAvailableLevelIndex = (GameManager.LastCompletedLevelIndex - firstLevelBuildIndex) + 1;
        Debug.Log(GameManager.LastCompletedLevelIndex);
        items = new LevelItem[SceneManager.sceneCountInBuildSettings - firstLevelBuildIndex];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(item, itemsList);
            items[i].Initialize(i, i + firstLevelBuildIndex, i <= lastAvailableLevelIndex);
        }
        UpdateItemsAvailability(lastAvailableLevelIndex);
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        SelectItem(0);  
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        playButton.SetActive(items[index].IsAvailable);
    }
}