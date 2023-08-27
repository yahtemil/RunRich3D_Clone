using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineCharacter : MonoBehaviour
{
    [HideInInspector]
    public bool enableBodyRotation;
    [HideInInspector]
    public bool canMoveForward;

    public List<CharacterOptions> characterOptions;
    public CharacterOptions selectOption;

    CharacterAnimationController _animatorController;
    public CharacterAnimationController animatorController => _animatorController == null ? _animatorController = GetComponentInChildren<CharacterAnimationController>() : _animatorController;

    CharacterClampController _clampController;
    public CharacterClampController clampController => _clampController == null ? _clampController = GetComponentInChildren<CharacterClampController>() : _clampController;

    SliderController _sliderController;
    public SliderController sliderController => _sliderController == null ? _sliderController = GetComponentInChildren<SliderController>() : _sliderController;

    CharacterMovementController _movementController;
    public CharacterMovementController movementController => _movementController == null ? _movementController = GetComponent<CharacterMovementController>() : _movementController;

    MoneyAnim _moneyAnim;
    public MoneyAnim moneyAnim => _moneyAnim == null ? _moneyAnim = GetComponentInChildren<MoneyAnim>() : _moneyAnim;

    [HideInInspector]
    public bool upgradeUp;

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.LevelStart.AddListener(LevelStart);
        GameManager.Instance.StageSuccess.AddListener(LevelEnd);
        GameManager.Instance.StageFail.AddListener(LevelEnd);
        selectOption = characterOptions[0];
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.LevelStart.RemoveListener(LevelStart);
        GameManager.Instance.StageSuccess.RemoveListener(LevelEnd);
        GameManager.Instance.StageFail.RemoveListener(LevelEnd);
    }

    public void LevelStart()
    {
        enableBodyRotation = true;
        canMoveForward = true;

    }

    public void LevelEnd()
    {
        enableBodyRotation = false;
        canMoveForward = false;
    }

    public void OnEnterWallArea(bool active)
    {
        clampController.SetClampActive(active);
    }

    public void CollectableTrigger(int addValue)
    {
        animatorController.PlayParticleSystem(addValue >= 0);
        if (sliderController.ChangeValue(addValue))
        {
            animatorController.SetWalkingAnimation(selectOption.animationName,upgradeUp);
        }
        AddMoney(addValue);
    }

    public void EnemyHit()
    {
        animatorController.PlayHitAnimation();
    }

    public void StopCharacter(float waitTime)
    {
        StartCoroutine(AgainMove(waitTime));
    }

    IEnumerator AgainMove(float waitTime)
    {
        LevelEnd();
        yield return new WaitForSeconds(waitTime);
        if(!LevelManager.Instance.GetLevelActive())
            LevelStart();
    }

    public void AddMoney(float addValue)
    {
        moneyAnim.MoneyAnimStart(addValue);
    }

    public void CompletedGame()
    {
        animatorController.PlayWinAnimation();
        LevelEnd();
    }
}
