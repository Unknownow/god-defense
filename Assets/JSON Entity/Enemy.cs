using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy : IComparable<Enemy>
{
    public readonly EnemyType enemyType;
    public readonly int spawnTime;
    public readonly int laneIndex;

    public int CompareTo(Enemy other)
    {
        return spawnTime.CompareTo(other.spawnTime);
    }
}
