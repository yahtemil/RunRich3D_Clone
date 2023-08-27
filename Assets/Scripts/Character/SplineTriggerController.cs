using Dreamteck.Splines;
using UnityEngine;

public class SplineTriggerController : MonoBehaviour
{
    public void OnEnterWallArea(SplineUser splineUser)
    {
        SplineCharacter splineCharacter = splineUser.GetComponent<SplineCharacter>();
        if (splineCharacter == null)
            return;

        splineCharacter.OnEnterWallArea(true);
    }

    public void OnExitWallArea(SplineUser splineUser)
    {
        SplineCharacter splineCharacter = splineUser.GetComponent<SplineCharacter>();
        if (splineCharacter == null)
            return;

        splineCharacter.OnEnterWallArea(false);
    }

    public void OnFinishTriggered(SplineUser splineUser)
    {
        SplineCharacter splineCharacter = splineUser.GetComponent<SplineCharacter>();
        if (splineCharacter == null)
            return;

        splineCharacter.CompletedGame();
        GameManager.Instance.LevelCompleted(true);
    }
}
