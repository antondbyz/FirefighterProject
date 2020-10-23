using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour 
{
    public void LoadScene(int sceneIndex) => SceneManager.LoadScene(sceneIndex);

    public void RestartCurrentScene() => SceneManager.LoadScene(gameObject.scene.buildIndex);    
}