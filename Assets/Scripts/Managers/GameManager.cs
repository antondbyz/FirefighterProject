using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public struct Layers
    {
        public const int IGNORE_EXTINGUISHING_SUBSTANCE = 8; 
        public const int GROUND = 9;
    }

    public static UnityEvent OnPaused = new UnityEvent();
    public static bool IsPaused { get; private set; } = false;

    public int CurrentSceneBuildIndex 
    {
        get { return currentSceneBuildIndex; }
        private set
        {
            currentSceneBuildIndex = value;
            SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(currentSceneBuildIndex));
            if(currentSceneBuildIndex == 1) userInterfaceManager.EnableUI(UIManager.UI.MainMenu);
            else if(currentSceneBuildIndex > 1) userInterfaceManager.EnableUI(UIManager.UI.MainGame);
        }
    }
    
    [SerializeField] private int sceneToLoad = 1;

    private UIManager userInterfaceManager;
    private int currentSceneBuildIndex;

    private void Awake()
    {
        userInterfaceManager = GetComponent<UIManager>();
    }

    private void Start()
    {
        if(SceneManager.sceneCount > 1) CurrentSceneBuildIndex = SceneManager.GetSceneAt(1).buildIndex;        
        else CurrentSceneBuildIndex = 0; // Scene with index 0 - GameController scene

        LoadScene(sceneToLoad);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SetPause(bool toPause)
    {
        if(toPause)
        {
            Time.timeScale = 0;
            OnPaused.Invoke();
        }
        else
        {
            Time.timeScale = 1;
        }
        IsPaused = toPause;
    }

    public void LoadScene(int index)
    {
        if(index <= 0) return; // You can't load scenes that don't exist or a GameController scene
        if(SceneManager.sceneCount > 2) return; // You can't have more than 2 open scenes simultaneously 
        
        if(CurrentSceneBuildIndex > 0) // You can't unload a GameController scene
            SceneManager.UnloadSceneAsync(CurrentSceneBuildIndex);

        SceneManager.LoadScene(index, LoadSceneMode.Additive);
        SceneManager.sceneLoaded += (Scene scene, LoadSceneMode mode) => CurrentSceneBuildIndex = scene.buildIndex;
    }
}