using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private SplineCharacter _splineCharacter;
    public SplineCharacter splineCharacter => _splineCharacter == null ? GetComponentInParent<SplineCharacter>() : _splineCharacter;

    public Animator animator;

    private string _animationName;

    public List<ParticleSystem> plusEffectList;
    public List<ParticleSystem> minusEffectList;
    public ParticleSystem plusAnimEffect;
    public ParticleSystem minusAnimEffect;

    public Queue<ParticleSystem> plusEffectQueue;
    public Queue<ParticleSystem> minusEffectQueue;

    public List<GameObject> WomanModels = new List<GameObject>();

    private void OnEnable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.LevelStart.AddListener(() => animator.Play("PoorWalk"));
        GameManager.Instance.StageFail.AddListener(PlayFailAnimation);
    }

    private void OnDisable()
    {
        if (Managers.Instance == null)
            return;

        LevelManager.Instance.LevelStart.RemoveListener(() => animator.Play("PoorWalk"));
        GameManager.Instance.StageFail.RemoveListener(PlayFailAnimation);
    }

    private void Awake()
    {
        _animationName = "PoorWalk";
        plusEffectQueue = new Queue<ParticleSystem>();
        minusEffectQueue = new Queue<ParticleSystem>();
        AddQueue(plusEffectList,plusEffectQueue);
        AddQueue(minusEffectList, minusEffectQueue);
    }

    private void AddQueue(List<ParticleSystem> list, Queue<ParticleSystem> queue)
    {
        foreach (var item in list)
        {
            queue.Enqueue(item);
        }
    }

    public void PlayParticleSystem(bool plus)
    {
        if (plus)
        {
            ParticleSystem ps = plusEffectQueue.Dequeue();
            ps.Play();
            plusEffectQueue.Enqueue(ps);
        }
        else
        {
            ParticleSystem ps = minusEffectQueue.Dequeue();
            ps.Play();
            minusEffectQueue.Enqueue(ps);
        }
    }

    public void ChangeEffectPlay(bool upgradeUp)
    {
        if (upgradeUp)
            plusAnimEffect.Play();
        else
            minusAnimEffect.Play();

        for (int i = 0; i < WomanModels.Count; i++)
        {
            WomanModels[i].SetActive(splineCharacter.selectOption == splineCharacter.characterOptions[i]);
        }
    }

    public void SetWalkingAnimation(string animationName,bool upgradeUp)
    {
        ChangeEffectPlay(upgradeUp);
        _animationName = animationName;
        animator.Play(upgradeUp ? "Change" : "Fail");
        StartCoroutine(TryPlayWalk(0.5f));
    }

    public void PlayHitAnimation()
    {
        animator.Play("Hit");
        StartCoroutine(TryPlayWalk(1f));
    }

    public void PlayFailAnimation()
    {
        animator.Play("Fail");
    }

    public void PlayWinAnimation()
    {
        if (splineCharacter.selectOption.characterValue >= 1)
            animator.Play("Dance");
        else
            animator.Play("Fail");
    }

    IEnumerator TryPlayWalk(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!LevelManager.Instance.GetLevelActive())
        {
            animator.Play(_animationName);
        }

    }
}
