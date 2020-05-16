using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMovement
{
    void StartMoving(Transform target);
    void StopMoving();
    void SlowDown(float slowPercentage);
    void BackToNormalSpeed();
}
