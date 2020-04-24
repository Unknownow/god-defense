using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private int _gridX;
    private int _gridY;
    private bool _isGround;
    public bool IsGround
    {
        get
        {
            return _isGround;
        }
    }
    private Vector3 _pos;
    public Vector3 Position
    {
        get
        {
            return this._pos;
        }
    }

    private Node parent
    {
        get
        {
            return this.parent;
        }
    }

    private int gCost;
    private int hCost;
    public int FCost
    {
        get
        {
            return gCost + hCost;
        }
    }

    public Node(bool IsGround, Vector3 pos, int gridX, int gridY)
    {
        this._isGround = IsGround;
        this._gridX = gridX;
        this._gridY = gridY;
        this._pos = pos;
    }
}
