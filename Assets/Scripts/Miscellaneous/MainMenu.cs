using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(int levelBuildIndex)
    {
        SceneManager.LoadScene(levelBuildIndex);
    }

    public void CloseTheGame()
    {
        Application.Quit();
    }
}