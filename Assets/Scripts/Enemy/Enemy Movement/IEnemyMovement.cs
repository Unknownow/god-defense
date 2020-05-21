using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void StartMoving(Transform target);
    void StopMoving();
    void PauseMoving();
    void ResumeMoving();
    void SlowDown(float slowPercentage);
    void BackToNormalSpeed();
    bool IsAtFinishLine { get; }
}
