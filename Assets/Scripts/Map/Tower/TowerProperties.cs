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
                Debug.Log("Damage " + value);
                _currentHitPoints -= value;
                _currentHitPoints = _currentHitPoints < 0 ? 0 : _currentHitPoints;
                if (_currentHitPoints <= 0)
                    _isDestroyed = true;
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

    public float Health {
        get {
            return Mathf.Min(_currentHitPoints/_hitPoints, 1.0f);
        }
    }
    private bool _isDestroyed;
    public bool IsDestroyed
    {
        get
        {
            return this._isDestroyed;
        }
    }

    private void Awake()
    {
        _isDestroyed = false;
        _currentHitPoints = _hitPoints;
    }
}
