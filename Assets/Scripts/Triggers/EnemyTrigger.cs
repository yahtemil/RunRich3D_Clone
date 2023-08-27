using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour,IInteractable
{
    bool triggerEnter;
    public void IEnterTrigger(SplineCharacter splineCharacter)
    {
        if (triggerEnter)
            return;

        triggerEnter = true;
        splineCharacter.StopCharacter(1f);
        splineCharacter.EnemyHit();
        splineCharacter.CollectableTrigger(-5);
    }
}
