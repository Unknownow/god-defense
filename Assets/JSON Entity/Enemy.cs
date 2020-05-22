using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Enemy : IComparable<Enemy>
{
    public EnemyType enemyType;
    public int spawnTime;
    public int laneIndex;

    public int CompareTo(Enemy other)
    {
        return this.spawnTime.CompareTo(other.spawnTime);
    }
}
