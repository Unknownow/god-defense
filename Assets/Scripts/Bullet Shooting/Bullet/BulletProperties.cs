using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProperties : MonoBehaviour
{
    [Header("Details")]
    [SerializeField]
    [Tooltip("Size of the bullet")]
    private int _size;
    public int Size
    {
        get
        {
            return this._size;
        }
    }
    
    [SerializeField]
    [Tooltip("The movement speed of the bullet")]
    private float _movementSpeed;
    public float MovementSpeed
    {
        get
        {
            return this._movementSpeed;
        }
    }
}
