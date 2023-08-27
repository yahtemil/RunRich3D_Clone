using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerController : MonoBehaviour
{
    SplineCharacter _splineCharacter;
    SplineCharacter SplineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable iInteractable))
            iInteractable.IEnterTrigger(SplineCharacter);
    }
}
