using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueGateTrigger : MonoBehaviour, IInteractable
{
    public void IEnterTrigger(SplineCharacter splineCharacter)
    {
        splineCharacter.CollectableTrigger(20);
        transform.parent.gameObject.SetActive(false);
    }
}
