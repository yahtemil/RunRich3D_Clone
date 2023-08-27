using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelManager : Singleton<LevelManager>
{
    [HideInInspector]
    public UnityEvent LevelStart;
    [HideInInspector]
    public UnityEvent LevelEnd;

    private bool _levelFinish;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        LevelStart.AddListener(StartLevel);
        LevelEnd.AddListener(EndLevel);
        GameManager.Instance.StageFail.AddListener(() => _levelFinish = true);
        GameManager.Instance.StageSuccess.AddListener(() => _levelFinish = true);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelStart.RemoveListener(StartLevel);
        LevelEnd.RemoveListener(EndLevel);
        GameManager.Instance.StageFail.RemoveListener(() => _levelFinish = true);
        GameManager.Instance.StageSuccess.RemoveListener(() => _levelFinish = true);
    }

    public void StartLevel()
    {
        GameManager.Instance.levelFinish = false;
        _levelFinish = false;
    }

    public void EndLevel()
    {
        GameManager.Instance.levelFinish = true;
        _levelFinish = true;
    }

    public bool GetLevelActive()
    {
        return _levelFinish;
    }

    public int GetLevelIndex()
    {
        int levelIndex = GameManager.Instance.levelValue;
        if(levelIndex > 3)
        {
            levelIndex = levelIndex % 3 == 0 ? 3 : levelIndex % 3;
            
        }

        return levelIndex;
    }

    public void NextLevel()
    {
        GameManager.Instance.levelValue++;
        SceneManager.Instance.NextLevel();
        UIManager.Instance.StartPanelOpen();
    }

    public void RestartLevel()
    {
        SceneManager.Instance.RestartLevel();
        UIManager.Instance.StartPanelOpen();
    }

}
