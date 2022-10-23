using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    [SerializeField] GameObject treePrefab;
    [SerializeField] GameObject treeWoodPrefab;
    [SerializeField] TerrainBlock terrain;
    [SerializeField] int treeCount = 1;

    private void Start()
    {
        List<Vector3> emptyPos = new List<Vector3>();
        GameObject spawnedTree = Random.value > 0.5f ? treePrefab : treeWoodPrefab;

        // get all empty position
        for (int x = -terrain.Extent; x < terrain.Extent; x++)
        {
            if (transform.position.z == 0 && x == 0)
            {
                continue;
            }
            emptyPos.Add(transform.position + Vector3.right * x);
        }

        // spawn tree
        for (int i = 0; i < treeCount; i++)
        {
            var index = Random.Range(0, emptyPos.Count);
            var spwanPos = emptyPos[index];
            Instantiate(spawnedTree, spwanPos, Quaternion.identity, this.transform);
            emptyPos.RemoveAt(index);
        }


        // spawn wood
        Instantiate
        (
            treePrefab,
            transform.position + Vector3.right * -(terrain.Extent + 1),
            Quaternion.identity,
            this.transform
        );

        Instantiate
        (
            treePrefab,
            transform.position + Vector3.right * (terrain.Extent + 1),
            Quaternion.identity,
            this.transform
        );

    }


}
