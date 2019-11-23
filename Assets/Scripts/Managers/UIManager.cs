using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UI { MainMenu, MainGame }

    [SerializeField] private GameObject mainGameUI = null;
    [SerializeField] private GameObject gameMenuPanel = null;
    [SerializeField] private GameObject mainMenuUI = null;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void SetActiveGameMenuPanel(bool value)
    {
        gameMenuPanel.SetActive(value);
        gameManager.SetPause(value);
    }

    public void EnableUI(UI userInterface)
    {
        switch(userInterface)
        {
            case UI.MainGame:
                mainMenuUI.SetActive(false);
                mainGameUI.SetActive(true);
                break;
            case UI.MainMenu:
                SetActiveGameMenuPanel(false);
                mainGameUI.SetActive(false);
                
                mainMenuUI.SetActive(true);
                break;
        }
    }
}
