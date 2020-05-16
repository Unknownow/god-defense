using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    North = 0b_0001,
    South = 0b_0010,
    East = 0b_0100,
    West = 0b_1000
}

public enum EnemyType
{
    Runner = 0,
    Heavier = 1,
    Tanker = 2,
}

public enum TrapType
{
    Booby = 0,
    Bomb = 1,
    Freeze = 2,
}