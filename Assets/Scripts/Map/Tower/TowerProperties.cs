using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerProperties : MonoBehaviour
{
    [SerializeField]
    private float _hitPoints;
    private float _currentHitPoints;
    public float CurrentHitPoints
    {
        get
        {
            return this._currentHitPoints;
        }
    }
    public float Hit
    {
        set
        {
            if (value > 0)
            {
                _currentHitPoints -= value;
                _currentHitPoints = _currentHitPoints < 0 ? 0 : _currentHitPoints;
            }
        }
    }

    public float Heal
    {
        set
        {
            if (value > 0)
            {
                _currentHitPoints += value;
                _currentHitPoints = _currentHitPoints < _hitPoints ? _currentHitPoints : _hitPoints;
            }
        }
    }
}
