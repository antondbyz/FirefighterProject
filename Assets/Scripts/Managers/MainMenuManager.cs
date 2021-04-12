using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void NewGame()
    {
        SaveManager.ResetGame();
        SceneLoader.ReplaceCurrentScene(gameObject.scene.buildIndex);
    }

    public void ChangeLanguage()
    {
        LocalizationManager.Instance.SwitchLocale();
    }
}