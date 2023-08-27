using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SliderController : MonoBehaviour
{
    private Slider _slider;
    public Slider slider => _slider == null ? GetComponent<Slider>() : _slider;

    public TextMeshProUGUI nameText;

    private SplineCharacter _splineCharacter;
    public SplineCharacter splineCharacter => _splineCharacter == null ? GetComponentInParent<SplineCharacter>() : _splineCharacter;

    int endValue = 0;

    public Image colorImage;

    int selectCharacterOptionIndex = 0;

    int deadValue = 0;

    private void Awake()
    {
        SliderChange();
        ColorChange();
    }

    public bool ChangeValue(int plusValue)
    {
        int _saveIndex = selectCharacterOptionIndex;
        endValue += plusValue;
        endValue = endValue >= 100 ? 100 : endValue <= 0 ? 0 : endValue;
        selectCharacterOptionIndex = endValue <= 33 ? 0 : endValue <= 66 ? 1 : 2;

        deadValue = endValue <= 0 ? deadValue + 1 : 0;
        if (deadValue >= 3)
            GameManager.Instance.LevelCompleted(false);

        SetSelectOption();
        SliderChange();
        ColorChange();
        if(_saveIndex != selectCharacterOptionIndex)
        {
            if (_saveIndex < selectCharacterOptionIndex)
                splineCharacter.upgradeUp = true;
            else
                splineCharacter.upgradeUp = false;

            SetText();
        }          

        return _saveIndex != selectCharacterOptionIndex;
    }

    private void SetSelectOption()
    {
        splineCharacter.selectOption = splineCharacter.characterOptions[selectCharacterOptionIndex];
    }


    private void SliderChange()
    {
        slider.DOValue(endValue, 0.25f);
    }

    private void ColorChange()
    {
        int firstColorIndex = selectCharacterOptionIndex == 2 ? 2 : selectCharacterOptionIndex;
        int secondColorIndex = selectCharacterOptionIndex == 2 ? 2 : selectCharacterOptionIndex + 1;
        Color firstColor = splineCharacter.characterOptions[firstColorIndex].characterColor;
        Color secondColor = splineCharacter.characterOptions[secondColorIndex].characterColor;
        float value = endValue == 0 ? 0f : endValue == 100 ? 1f : (endValue % 33) / 33f == 0 ? 1f : (endValue % 33) / 33f;
        Color targetColor = Color.Lerp(firstColor, secondColor, value);

        colorImage.DOColor(targetColor, 0.25f);
    }

    private void SetText()
    {
        nameText.text = splineCharacter.selectOption.characterName;
    }

}
