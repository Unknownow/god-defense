using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyVisualEffect
{
    void Init();
    void Freeze(float percentage);
    void Unfreeze();
}
