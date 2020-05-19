using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Damage bullet deals to enemy")]
    private float _bulletDamage;
    public float BulletDamage
    {
        get
        {
            return this._bulletDamage;
        }
    }
    [SerializeField]
    [Tooltip("The movement speed of the bullet")]
    private float _bulletSpeed;
    public float BulletSpeed
    {
        get
        {
            return this._bulletSpeed;
        }
    }

    [SerializeField]
    [Tooltip("Bullet's lifetime")]
    private float _lifeTime;
    public float LifeTime
    {
        get
        {
            return this._lifeTime;
        }
    }
}
