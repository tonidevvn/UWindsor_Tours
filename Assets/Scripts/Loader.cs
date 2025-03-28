using System;
using UnityEngine;
using UnityEngine.UIElements;

public static class Loader
{
    private class loadingMonoBehaviour : MonoBehaviour { }

    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void LoadScene(string scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            loadingGameObject.AddComponent<loadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        UnityEngine.SceneManagement.SceneManager.LoadScene("LoadingMenu");
    }

    private static System.Collections.IEnumerator LoadSceneAsync(string scene)
    {
        yield return null;
        loadingAsyncOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scene);
        while (!loadingAsyncOperation.isDone)
        {
            yield return null;
        }
    }

    public static float GetLoadingProgress()
    {
        if (loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        }
        else
        {
            return 1f;
        }
    }

    public static void LoaderCallback()
    {
        if (onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }

    public static void LoadGame()
    {
        LoadScene("Game");
    }

    public static void LoadCharacterSelection()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("CharacterSelection");
    }

    public static void LoadMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public static void LoadSettingsMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
    }
}