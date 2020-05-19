using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    [SerializeField]
    private Direction _playerDirection;
    public Vector3 PlayerDirection
    {
        get
        {
            Vector3 dir = Vector3.zero;
            switch (this._playerDirection)
            {
                case Direction.North:
                    dir = -Vector3.forward;
                    break;
                case Direction.South:
                    dir = Vector3.forward;
                    break;
                case Direction.East:
                    dir = Vector3.right;
                    break;
                case Direction.West:
                    dir = -Vector3.right;
                    break;
            }
            return dir;
        }
    }
}
