using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProperties : MonoBehaviour
{
    [SerializeField]
    private float _hitPoints;
    public float HitPoints
    {
        get
        {
            return this._hitPoints;
        }
    }
    public float Hit
    {
        set
        {
            if (value > 0)
            {
                this._hitPoints -= value;
                this._hitPoints = this._hitPoints < 0 ? 0 : this._hitPoints;
            }
        }
    }

    public float Heal
    {
        set
        {
            if (value > 0)
                this._hitPoints += value;
        }
    }
}
