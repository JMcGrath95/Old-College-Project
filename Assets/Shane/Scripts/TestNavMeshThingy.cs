using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestNavMeshThingy : MonoBehaviour
{
    private void Start()
    {
        GetComponent<NavMeshSurface>().BuildNavMesh();
    }

}
