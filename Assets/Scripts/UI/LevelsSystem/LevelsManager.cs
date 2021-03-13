using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : UI_Manager<LevelItem>
{
    [SerializeField] private int firstLevelBuildIndex = 2;
    [SerializeField] private LevelItem item = null;
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private GameObject playButton = null;

    public void PlaySelectedLevel() => GameManager.LoadScene(SelectedItem.LevelBuildIndex);

    private void Awake() 
    {
        items = new LevelItem[SceneManager.sceneCountInBuildSettings - firstLevelBuildIndex];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(item, itemsList);
            items[i].Initialize(i, i + firstLevelBuildIndex);
        }
        LastAvailableItemIndex = (GameManager.LastCompletedLevelIndex - firstLevelBuildIndex) + 1;
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        SelectItem(LastAvailableItemIndex);  
    }

    protected override void SelectItem(int index)
    {
        base.SelectItem(index);
        playButton.SetActive(items[index].IsAvailable);
    }
}