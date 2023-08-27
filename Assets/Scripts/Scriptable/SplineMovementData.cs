using UnityEngine;

[CreateAssetMenu(menuName = "Game Datas/Spline Movement Data", order = 0)]
public class SplineMovementData : ScriptableObject
{
    public float DefaultSpeed = 6;
    public float SlideSpeed = 8;
    [Tooltip("The blend duration that uses when character speed changed.")]
    public float SpeedBlendDuration = 1;
}
