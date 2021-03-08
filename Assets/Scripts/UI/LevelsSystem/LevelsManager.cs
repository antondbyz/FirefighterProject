using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour 
{
    [SerializeField] private LevelItem item = null;
    [SerializeField] private Transform itemsList = null;
    [SerializeField] private int firstLevelIndex = 2;

    private LevelItem[] levels;

    private void Awake() 
    {
        levels = new LevelItem[SceneManager.sceneCountInBuildSettings - firstLevelIndex];
        for(int i = 0; i < levels.Length; i++)
        {
            levels[i] = Instantiate(item, itemsList);
            levels[i].Initialize(i + 1);
        }
    }
}