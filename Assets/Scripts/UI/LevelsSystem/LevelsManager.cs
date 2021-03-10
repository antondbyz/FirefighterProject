using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : UI_Manager<LevelItem>
{
    [SerializeField] private LevelItem item = null;
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private int firstLevelIndex = 2;

    public void PlaySelectedLevel() => GameManager.LoadScene(selectedItem.LevelIndex);

    private void Awake() 
    {
        items = new LevelItem[SceneManager.sceneCountInBuildSettings - firstLevelIndex];
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = Instantiate(item, itemsList);
            items[i].Initialize(i, i + firstLevelIndex);
        }
    }

    protected override void OnEnable() 
    {
        base.OnEnable();
        SelectItem(0);    
    }
}