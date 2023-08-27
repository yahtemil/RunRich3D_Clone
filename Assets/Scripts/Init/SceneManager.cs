using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : Singleton<SceneManager>
{
    string loadSceneName = "level";

    private void Start()
    {
        loadSceneName = "level" + LevelManager.Instance.GetLevelIndex().ToString();
        StartCoroutine(StartWait());
    }

    IEnumerator StartWait()
    {
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName, LoadSceneMode.Additive);
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartLevelTiming());
    }

    public void NextLevel()
    {
        StartCoroutine(NextLevelTiming());
    }

    IEnumerator NextLevelTiming()
    {
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(loadSceneName);
        loadSceneName = "level" + LevelManager.Instance.GetLevelIndex().ToString();
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName,LoadSceneMode.Additive);
    }

    IEnumerator RestartLevelTiming()
    {
        yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(loadSceneName);
        yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName,LoadSceneMode.Additive);
    }
}
