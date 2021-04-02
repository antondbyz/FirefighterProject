using UnityEngine;

public class ScenesManager : MonoBehaviour 
{
    public void LoadScene(int sceneIndex) => SceneLoader.ReplaceCurrentScene(sceneIndex);

    public void RestartCurrentScene() => SceneLoader.ReplaceCurrentScene(gameObject.scene.buildIndex);   

    public void ToTheMainMenu() => LoadScene(1); 
}