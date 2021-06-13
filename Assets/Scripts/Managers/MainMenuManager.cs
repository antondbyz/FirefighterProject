using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SaveManager.ResetGame();
        SceneLoader.ReplaceCurrentScene(gameObject.scene.buildIndex);
    }
}