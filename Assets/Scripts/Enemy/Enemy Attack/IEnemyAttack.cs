using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttack
{
    void StartAttack();
    void StopAttack();
    void SlowDown(float percentage);
    void BackToNormalAttackRate();
}