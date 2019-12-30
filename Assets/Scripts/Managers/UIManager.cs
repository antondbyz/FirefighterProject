using UnityEngine;

public class UIManager : MonoBehaviour
{
    public enum UI { MainMenu, MainGame }

    [SerializeField] private MainGameUI mainGameUI = null;
    [SerializeField] private MainMenuUI mainMenuUI = null;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void EnableUI(UI userInterface)
    {
        switch(userInterface)
        {
            case UI.MainGame:
                mainMenuUI.gameObject.SetActive(false);
                mainGameUI.gameObject.SetActive(true);
                break;
            case UI.MainMenu:
                mainGameUI.gameObject.SetActive(false);
                mainMenuUI.gameObject.SetActive(true);
                break;
        }
    }
}
