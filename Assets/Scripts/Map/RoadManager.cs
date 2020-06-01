using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoadManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _road;

    private void Awake()
    {
        NavMeshSurface[] surfaces = _road.GetComponents<NavMeshSurface>();
        foreach (NavMeshSurface surface in surfaces)
        {
            surface.BuildNavMesh();
        }
    }
}
