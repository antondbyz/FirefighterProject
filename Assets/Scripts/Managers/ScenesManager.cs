using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static int CurrentSceneBuildIndex
    {
        get => currentSceneBuildIndex;
        set
        {
            currentSceneBuildIndex = value;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex));
        }
    }
    private static int currentSceneBuildIndex;

    private void Awake() 
    {
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => CurrentSceneBuildIndex = scene.buildIndex;    
    } 

    public static void ChangeScene(int index)
    {
        if(SceneManager.sceneCount > 1)
            SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(1));
        SceneManager.LoadScene(index, LoadSceneMode.Additive);
    }
}