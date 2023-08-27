using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject StartedPanel;
    public GameObject InGamePanel;
    public GameObject FailPanel;
    public GameObject CompletedPanel;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        GameManager.Instance.StageSuccess.AddListener(CompletedPanelOpen);
        GameManager.Instance.StageFail.AddListener(FailPanelOpen);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        GameManager.Instance.StageSuccess.RemoveListener(CompletedPanelOpen);
        GameManager.Instance.StageFail.RemoveListener(FailPanelOpen);
    }

    public void StartPanelOpen()
    {
        StartedPanel.SetActive(true);
        InGamePanel.SetActive(false);
        FailPanel.SetActive(false);
        CompletedPanel.SetActive(false);
    }

    public void InGamePanelOpen()
    {
        StartedPanel.SetActive(false);
        InGamePanel.SetActive(true);
        FailPanel.SetActive(false);
        CompletedPanel.SetActive(false);
    }

    public void FailPanelOpen()
    {
        StartedPanel.SetActive(false);
        InGamePanel.SetActive(false);
        FailPanel.SetActive(true);
        CompletedPanel.SetActive(false);
    }

    public void CompletedPanelOpen()
    {
        StartedPanel.SetActive(false);
        InGamePanel.SetActive(false);
        FailPanel.SetActive(false);
        CompletedPanel.SetActive(true);
    }

    public void StartButton()
    {
        InGamePanelOpen();
        LevelManager.Instance.LevelStart.Invoke();
    }

    public void FailButton()
    {
        LevelManager.Instance.RestartLevel();
    }

    public void CompletedButton()
    {
        LevelManager.Instance.NextLevel();
    }

}
