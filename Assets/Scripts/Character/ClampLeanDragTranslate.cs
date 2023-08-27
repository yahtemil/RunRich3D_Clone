using Lean.Touch;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampLeanDragTranslate : MonoBehaviour
{
    public SplineClampData clampData;
    public float rotateSpeed => clampData.RotateSpeed;
    public float recoverySpeed => clampData.RecoverySpeed;
	public float minRotateAngle => clampData.MinRotateAngle;
	public float maxRotateAngle => clampData.MaxRotateAngle;
	public float sensitivity => clampData.Sensitivity;
	public float movementWidth => clampData.MovementWidth;

	public LeanFingerFilter Use = new LeanFingerFilter(true);

	public Camera _camera;

	public float Dampening = -1.0f;

	private Vector3 _remainingTranslation;

	private Vector3 _targetRotation;

	float _minValue;
	float _maxValue;

	float xClamp;

	public virtual void Awake()
	{
		Use.UpdateRequiredSelectable(gameObject);
		_minValue = -(movementWidth / 2f);
		_maxValue = movementWidth / 2f;
	}

	// Update is called once per frame
	public virtual void Update()
    {
		var oldPosition = transform.localPosition;

		// Get the fingers we want to use
		var fingers = Use.UpdateAndGetFingers();

		// Calculate the screenDelta value based on these fingers
		var screenDelta = LeanGesture.GetScreenDelta(fingers);

		if (screenDelta != Vector2.zero)
		{
			// Perform the translation
			if (transform is RectTransform)
			{

			}
			else
			{
				Translate(screenDelta);
				Delta(screenDelta);
			}
		}

		// Increment
		_remainingTranslation += transform.localPosition - oldPosition;

		// Get t value
		var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

		// Dampen remainingDelta
		var newRemainingTranslation = Vector3.Lerp(_remainingTranslation, Vector3.zero, factor);

		// Shift this transform by the change in delta
		transform.localPosition = oldPosition + _remainingTranslation - newRemainingTranslation;

		// Update remainingDelta with the dampened value
		_remainingTranslation = newRemainingTranslation;
	}

	private void Translate(Vector2 screenDelta)
	{
		// Make sure the camera exists
		var camera = LeanTouch.GetCamera(_camera, gameObject);

		if (camera != null)
		{
			// Screen position of the transform
			var screenPoint = camera.WorldToScreenPoint(transform.position);

			// Add the deltaPosition
			screenPoint += (Vector3)screenDelta * sensitivity;

			// Convert back to world space			
			Vector3 position = camera.ScreenToWorldPoint(screenPoint);

			transform.localPosition = Clamp(position);

		}
		else
		{
			Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
		}
	}

	public virtual void Delta(Vector3 screenDelta)
	{

	}

	public void SetClampActive(bool active)
    {
		_minValue = active == false ? -(movementWidth / 2f) : transform.localPosition.x >= 0f ? 0f : -(movementWidth / 2f);
		_maxValue = active == false ? (movementWidth / 2f) : transform.localPosition.x >= 0f ? (movementWidth / 2f) : 0f;
    }


	private Vector3 Clamp(Vector3 position)
	{
		Vector3 localPositon;
		Transform parent = transform.parent;

		if (parent != null)
			localPositon = parent.InverseTransformPoint(position);
		else
			localPositon = position;

		Vector3 playerPos = transform.localPosition;

		float xPosition = localPositon.x;
		xClamp = movementWidth / 2f;

		xPosition = Mathf.Clamp(xPosition, _minValue, _maxValue);

		playerPos.x = xPosition;

		return playerPos;
	}
}
