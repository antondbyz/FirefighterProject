using UnityEngine;

public class MainGameUI : MonoBehaviour
{
    [SerializeField] private GameObject gameMenuPanel = null;
    private GameManager gameManager;

    private void Awake()
    {
        GameObject gameController = GameObject.FindWithTag("GameController");
        gameManager = gameController.GetComponent<GameManager>();
    }

    public void SetActiveGameMenuPanel(bool value)
    {
        gameMenuPanel.SetActive(value);
        gameManager.SetPause(value);
    }

    public void OnEnable()
    {
        SetActiveGameMenuPanel(false);
    }
}
