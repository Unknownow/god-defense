using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid<T>
{
    private int _gridSizeX;
    private int _gridSizeY;
    private T[,] _grid;

    public Grid(int gridSizeX, int gridSizeY)
    {
        this._gridSizeX = gridSizeX;
        this._gridSizeY = gridSizeY;
        this._grid = new T[gridSizeX, gridSizeY];
    }


    public T[,] GetGrid()
    {
        return _grid;
    }

    public T GetElement(int x, int y)
    {
        return _grid[x, y];
    }

    public void SetElement(int x, int y, T element)
    {
        _grid[x, y] = element;
    }
}
