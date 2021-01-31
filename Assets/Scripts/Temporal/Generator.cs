using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    #region GROUND
    public int numMaxGroundAreas = 3;
    public GameObject groundPrefab;

    private List<GameObject> groundList;
    private float groundDepthMagnitude;
    #endregion

    #region WALLS
    public int numMaxWalls = 5;
    public Vector3 firstWallPos;
    public GameObject[] wallPrefabs;

    private List<GameObject> wallList;
    #endregion

    private void Awake()
    {
       
        // crear lista de terrenos
        groundList = new List<GameObject>(numMaxGroundAreas);
        // rellenar la lista de terrenos
        for (int i = 0; i < numMaxGroundAreas; ++i)
            groundList.Add(Instantiate(groundPrefab, transform));
        // longitud del terreno
        groundDepthMagnitude = groundPrefab.transform.localScale.z;
        // los colocamos en su lugar
        for (int i = 0; i < groundList.Count; ++i)
            groundList[i].transform.position = new Vector3(0, 0, i * groundDepthMagnitude);

        // crear lista de muros
        wallList = new List<GameObject>(wallPrefabs.Length);
        // rellenar la lista de muros
        for (int i = 0; i < numMaxWalls; ++i)
        {
            int prefabRandomIndex = (int) Mathf.Round(Random.Range(0, wallPrefabs.Length));

            wallList.Add(Instantiate(wallPrefabs[prefabRandomIndex], transform));
        }

        // los colocamos en su lugar

        // el primer muro no depende de nadie, tenemos que poner su valor inicial
        if (wallList.Count > 0)
            wallList[0].transform.position = firstWallPos;

        // el resto de muros siempre dependeran del muro anterior
        for (int i = 1; i < wallList.Count; ++i)
        {
            GameObject lastWall = wallList[i - 1];
            GameObject thisWall = wallList[i];

            Bounds lastWallBounds = lastWall.GetComponent<Collider>().bounds;
            Bounds thisWallBounds = thisWall.GetComponent<Collider>().bounds;

            float newX = lastWall.transform.position.x;
            float newY = lastWall.transform.position.y;
            float newZ = lastWall.transform.position.z + Mathf.Abs(lastWallBounds.max.z) + Mathf.Abs(thisWallBounds.min.z);

            thisWall.transform.position = new Vector3(newX, newY, newZ);
        }
    }

    public void GenerateGround()
    {
        Vector3 lastItemPosition = groundList[groundList.Count - 1].transform.position;
        Vector3 translation = lastItemPosition + new Vector3(0, 0, groundDepthMagnitude);

        GameObject instance = Instantiate(groundPrefab, transform);
        instance.transform.Translate(translation);

        groundList.Add(instance);
    }

    public void GenerateWall()
    {
        int prefabRandomIndex = (int) Mathf.Round(Random.Range(0, wallPrefabs.Length));

        GameObject lastWall = wallList[wallList.Count - 1];
        GameObject newWall = Instantiate(wallPrefabs[prefabRandomIndex], transform);

        Bounds lastWallBounds = lastWall.GetComponent<Collider>().bounds;
        Bounds thisWallBounds = newWall.GetComponent<Collider>().bounds;

        float newX = lastWall.transform.position.x;
        float newY = lastWall.transform.position.y;
        float newZ = lastWallBounds.max.z + Mathf.Abs(thisWallBounds.min.z);

        newWall.transform.position = new Vector3(newX, newY, newZ);

        wallList.Add(newWall);
    }
}
