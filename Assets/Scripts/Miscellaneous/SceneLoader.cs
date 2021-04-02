using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneLoader : MonoBehaviour 
{
    private const int LoadingSceneIndex = 2;
    private static int sceneIndexToLoad;
    private static WaitForSeconds minLoadingDuration = new WaitForSeconds(1);

    [SerializeField] private TMP_Text loadingValueText = null;

    public static void ReplaceCurrentScene(int newSceneIndex)
    {
        sceneIndexToLoad = newSceneIndex;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(LoadingSceneIndex, LoadSceneMode.Additive);
    }

    private void Awake() 
    {
        StartCoroutine(LoadScene());   
    }   

    private void OnEnable() {
        
    }

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndexToLoad, LoadSceneMode.Additive);
        operation.completed += (AsyncOperation o) => SceneManager.UnloadSceneAsync(gameObject.scene);
    } 
}