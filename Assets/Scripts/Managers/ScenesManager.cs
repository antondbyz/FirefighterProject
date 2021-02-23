using UnityEngine;

public class ScenesManager : MonoBehaviour 
{
    public void LoadScene(int sceneIndex) => GameManager.LoadScene(sceneIndex);

    public void RestartCurrentScene() => GameManager.LoadScene(gameObject.scene.buildIndex);   

    public void ToTheMainMenu() => LoadScene(1); 
}