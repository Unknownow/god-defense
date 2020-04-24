using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesGrid : MonoBehaviour
{
    private Grid<Node> _nodesGrid;
    [SerializeField]
    private Transform _startPos;
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]
    private Vector2 _gridWorldSize;
    [SerializeField]
    private float _nodeRadius;
    [SerializeField]
    private float _distance;

    private float _nodeDiameter;

    private void Start()
    {
        _nodeDiameter = _nodeRadius * 2;
        int gridSizeX = Mathf.RoundToInt(_gridWorldSize.x / _nodeDiameter);
        int gridSizeY = Mathf.RoundToInt(_gridWorldSize.y / _nodeDiameter);

        CreateGrid(gridSizeX, gridSizeY);
    }

    private void CreateGrid(int gridSizeX, int gridSizeY)
    {
        _nodesGrid = new Grid<Node>(gridSizeX, gridSizeY);
        Vector3 bottomLeftPos = transform.position - Vector3.right * _gridWorldSize.x / 2 - Vector3.forward * _gridWorldSize.y / 2;
        bool isGround = false;
        for (int i = 0; i < gridSizeX; i++)
        {
            for (int j = 0; j < gridSizeY; j++)
            {
                Vector3 nodePos = bottomLeftPos + (i * _nodeDiameter + _nodeRadius) * Vector3.right + (j * _nodeDiameter + _nodeRadius) * Vector3.forward;
                isGround = false;
                if (Physics.CheckSphere(nodePos, _nodeRadius / 2, _groundMask))
                {
                    isGround = true;
                }
                _nodesGrid.SetElement(i, j, new Node(isGround, nodePos, i, j));
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireCube(transform.position, new Vector3(_gridWorldSize.x, 1, _gridWorldSize.y));
        if (_nodesGrid != null)
        {
            foreach (Node n in _nodesGrid.GetGrid())
            {
                if (n.IsGround)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.gray;
                }

                Gizmos.DrawCube(new Vector3(n.Position.x, .4f, n.Position.z), new Vector3(_nodeDiameter - _distance, .2f, _nodeDiameter - _distance));
            }
        }
    }
}
