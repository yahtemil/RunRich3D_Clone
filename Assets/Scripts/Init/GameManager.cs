using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public GameSettings gameSettings;

    [HideInInspector]
    public UnityEvent GameStart;
    [HideInInspector]
    public UnityEvent GameEnd;
    [HideInInspector]
    public UnityEvent StageSuccess = new UnityEvent();
    [HideInInspector]
    public UnityEvent StageFail = new UnityEvent();

    [HideInInspector]
    public float goldValue;
    [HideInInspector]
    public int levelValue;

    //[HideInInspector]
    public bool levelFinish;


    private void Awake()
    {
        goldValue = PlayerPrefs.GetFloat("gold", 0);
        levelValue = PlayerPrefs.GetInt("level", 1);
    }


    public void LevelCompleted(bool completed)
    {
        if (levelFinish)
            return;

        levelFinish = true;

        if (completed)
            StageSuccess.Invoke();
        else
            StageFail.Invoke();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetInt("level", levelValue);
            PlayerPrefs.SetFloat("gold", goldValue);
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("level", levelValue);
        PlayerPrefs.SetFloat("gold", goldValue);
    }

}
