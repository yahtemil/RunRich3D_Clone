using Dreamteck.Splines;
using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{

    SplineFollower _splineFollower;
    SplineFollower splineFollower => _splineFollower == null ? _splineFollower = GetComponentInChildren<SplineFollower>() : _splineFollower;

    SplineCharacter _splineCharacter;
    SplineCharacter splineCharacter => _splineCharacter == null ? _splineCharacter = GetComponent<SplineCharacter>() : _splineCharacter;

    public SplineMovementData characterMovement;

    float _currentSpeed;

    private void Awake()
    {
        SetupDefaultValues();
    }

    private void SetupDefaultValues()
    {
        _currentSpeed = characterMovement.DefaultSpeed;
    }


    private void Update()
    {
        if (!splineCharacter.canMoveForward)
        {
            return;
        }

        splineFollower.Move(_currentSpeed * Time.deltaTime);
    }
}
