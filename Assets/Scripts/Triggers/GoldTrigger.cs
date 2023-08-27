using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldTrigger : MonoBehaviour, IInteractable
{
    public void IEnterTrigger(SplineCharacter splineCharacter)
    {
        splineCharacter.CollectableTrigger(1);
        gameObject.SetActive(false);
    }
}
