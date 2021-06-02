using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour 
{
    private const int LoadingSceneIndex = 2;
    private static int sceneIndexToLoad;

    public static void ReplaceCurrentScene(int newSceneIndex)
    {
        sceneIndexToLoad = newSceneIndex;
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());
        SceneManager.LoadScene(LoadingSceneIndex, LoadSceneMode.Additive);
    }

    public static void LoadNewScene(int newSceneIndex)
    {
        sceneIndexToLoad = newSceneIndex;
        SceneManager.LoadScene(LoadingSceneIndex, LoadSceneMode.Additive);
    }

    private void Awake() => StartCoroutine(LoadScene());

    private IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndexToLoad, LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(1);
        operation.allowSceneActivation = true;
        if(operation.isDone) SceneManager.UnloadSceneAsync(gameObject.scene);
        else operation.completed += (AsyncOperation o) => SceneManager.UnloadSceneAsync(gameObject.scene);
    } 
}