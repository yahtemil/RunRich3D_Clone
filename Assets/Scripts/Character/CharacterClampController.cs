using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterClampController : ClampLeanDragTranslate
{
    SplineCharacter _splineCharacter;
    SplineCharacter splineCharacter => _splineCharacter == null ? _splineCharacter = GetComponentInParent<SplineCharacter>() : _splineCharacter;

    public Transform rotateBody;
    private Vector3 _targetRotation;


    public override void Update()
    {
        if(splineCharacter.canMoveForward)

        base.Update();
        Rotate();
    }

    private void Rotate()
    {
        if (!splineCharacter.enableBodyRotation)
            return;

        rotateBody.localRotation = Quaternion.Slerp(rotateBody.localRotation, Quaternion.Euler(_targetRotation), Time.deltaTime * rotateSpeed);
        _targetRotation.y = Mathf.Lerp(_targetRotation.y, 0f, Time.deltaTime * recoverySpeed);
    }


    public override void Delta(Vector3 screenDelta)
    {
		if (splineCharacter.enableBodyRotation)
		{
			_targetRotation.y += screenDelta.x;
			_targetRotation.y = Mathf.Clamp(_targetRotation.y, minRotateAngle, maxRotateAngle);
		}
	}
}
