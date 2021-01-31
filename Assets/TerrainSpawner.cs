using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public Transform parentGroup;
    public GameObject[] terrainPrefabs;

    private void Start()
    {
        InvokeRepeating("GenerateTerrainPrefab", 3f, 3f);
    }

    private void GenerateTerrainPrefab()
    {
        int randomPrefabIndex = (int)Mathf.Round(Random.Range(0, terrainPrefabs.Length));

        GameObject instance = Instantiate(terrainPrefabs[randomPrefabIndex], parentGroup);

        instance.transform.position = parentGroup.transform.position + new Vector3(Random.Range(-10f, 10f), 1, Random.Range(0, 10f));
    }
}
