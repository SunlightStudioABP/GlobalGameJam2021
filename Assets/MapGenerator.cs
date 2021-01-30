using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject wallLeft, wallRight, floorPrefab;

    [SerializeField]
    GameObject biir;

    [SerializeField]
    float wallTime = 4f, floorTime = 20f;

    private void Start()
    {
        InvokeRepeating("PlaceWall", 0f, wallTime);
        InvokeRepeating("PlaceFloor", 0f, floorTime);
    }

    private void PlaceWall()
    {
        Instantiate(wallLeft, transform, true);
        Instantiate(wallRight, transform, true);
    }

    private void PlaceFloor()
    {
        Instantiate(floorPrefab, transform, true);
    }
}
