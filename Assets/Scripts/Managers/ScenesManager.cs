using UnityEngine;

public class ScenesManager : MonoBehaviour 
{
    public static ScenesManager Instance;

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of ScenesManager!");    
    }

    public void LoadScene(int sceneIndex) => SceneLoader.ReplaceCurrentScene(sceneIndex);

    public void RestartCurrentScene() => SceneLoader.ReplaceCurrentScene(gameObject.scene.buildIndex);   

    public void ToTheMainMenu() => LoadScene(1);
}