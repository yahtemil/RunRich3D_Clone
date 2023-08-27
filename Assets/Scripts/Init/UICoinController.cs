using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class UICoinController : Singleton<UICoinController>
{
    public TextMeshProUGUI goldText;
    double _currentBalance;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.Instance.MoneyUpdate.AddListener(UpdateBalance);
        SetGoldText();
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        EventManager.Instance.MoneyUpdate.RemoveListener(UpdateBalance);
    }

    public void SetGoldText()
    {
        _currentBalance = GameManager.Instance.goldValue;
        UpdateBalance();
    }

    private void UpdateBalance()
    {
        double targetAmount = GameManager.Instance.goldValue;

        DOTween.Kill("coin");
        DOTween.To(() => _currentBalance, x => _currentBalance = x, targetAmount, 0.2f).SetEase(Ease.Linear).SetId("coin").OnUpdate(() =>
        {
            goldText.text = ScoreShowF2(targetAmount);
        });
    }

    public static string ScoreShowF2(double Score)
    {
        string result;
        string[] ScoreNames = new string[] { "", "k", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", "af", "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay", "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br", "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", };
        int i;

        for (i = 0; i < ScoreNames.Length; i++)
            if (Score < 900)
                break;
            else Score /= 1000f;

        if (Score == System.Math.Floor(Score))
            result = Score.ToString() + ScoreNames[i];
        else result = Score.ToString("F2") + ScoreNames[i];
        return result;
    }
}
