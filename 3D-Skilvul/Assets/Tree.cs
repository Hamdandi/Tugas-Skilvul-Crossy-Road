using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public static List<Vector3> AllPositions = new List<Vector3>();

    private void OnEnable()
    {
        AllPositions.Add(transform.position);
    }

    private void OnDisable()
    {
        AllPositions.Remove(transform.position);
    }
}
