using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;

public class MoneyAnim : MonoBehaviour
{
    public SpriteRenderer dolarIcon;
    public TextMeshPro MoneyText;
    float _moneyValue;
    Vector3 _localPosition;
    bool active;
    int checkCounter = 0;

    private void Awake()
    {
        _moneyValue = GameManager.Instance.gameSettings.addMoneyValue;
        _localPosition = transform.localPosition;
        MoneyText.DOFade(0f, 0f);
        dolarIcon.DOFade(0f, 0f);
    }

    private void OnEnable()
    {
        _moneyValue = GameManager.Instance.gameSettings.addMoneyValue;
    }

    public void MoneyAnimStart(float goldValue)
    {

        if (!active)
        {
            active = true;
            _moneyValue = 0;
        }

        GameManager.Instance.goldValue += goldValue;
        EventManager.Instance.MoneyUpdate.Invoke();

        _moneyValue += goldValue;
        MoneyText.text = (_moneyValue >= 0 ? "+" : "") + _moneyValue.ToString("F0");
        Color targetColor = _moneyValue >= 0f ? Color.green : Color.red;
        MoneyText.color = targetColor;
        checkCounter++;
        StartCoroutine(CheckTiming(checkCounter));

        transform.DOKill();
        MoneyText.DOKill();
        dolarIcon.DOKill();

        SetInitialState();

        transform.DOLocalMoveY(1.85f, 1f);
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        MoneyText.DOFade(0f, 2f).SetEase(Ease.InQuint);
        dolarIcon.DOFade(0f, 2f).SetEase(Ease.InQuint);

        GameManager.Instance.goldValue += _moneyValue;
    }

    IEnumerator CheckTiming(int _checkCounter)
    {
        yield return new WaitForSeconds(2f);
        if (_checkCounter == checkCounter)
            active = false;
    }

    private void SetInitialState()
    {
        transform.localPosition = _localPosition;
        transform.localScale = Vector3.one;
        MoneyText.DOFade(1f, 0f);
        dolarIcon.DOFade(1f, 0f);
    }
}
