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
    public int numMaxWallsPerSide = 5;
    public Vector3 firstLeftWallPos;
    public Vector3 firstRightWallPos;
    public GameObject[] wallPrefabs;

    // en la lista se guardaran los muros 0-izq, 1-der, 2-izq, 3-der...
    private List<GameObject> wallList;
    #endregion

    private void Awake()
    {
       
        // crear lista de terrenos
        groundList = new List<GameObject>();
        // rellenar la lista de terrenos
        for (int i = 0; i < numMaxGroundAreas; ++i)
            groundList.Add(Instantiate(groundPrefab, transform));
        // longitud del terreno
        groundDepthMagnitude = groundPrefab.transform.localScale.z;
        // los colocamos en su lugar
        for (int i = 0; i < groundList.Count; ++i)
            groundList[i].transform.position = new Vector3(0, 0, i * groundDepthMagnitude);

        // crear lista de muros
        wallList = new List<GameObject>();
        // rellenar la lista de muros
        for (int i = 0; i < numMaxWallsPerSide; ++i)
        {
            int prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));
            wallList.Add(Instantiate(wallPrefabs[prefabRandomIndex], transform)); // izq

            prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));
            wallList.Add(Instantiate(wallPrefabs[prefabRandomIndex], transform)); // der
        }

        // los colocamos en su lugar

        // el primer muro no depende de nadie, tenemos que poner su valor inicial
        if (wallList.Count > 0)
        {
            wallList[0].transform.position = firstLeftWallPos;  // izq
            wallList[1].transform.position = firstRightWallPos; // der
        }

        // el resto de muros siempre dependeran del muro anterior (2 anteriores, ya que alternan izq-der-izq-der...)
        for (int i = 2; i < wallList.Count; ++i)
        {
            GameObject lastWall = wallList[i - 2];
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
        int prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));

        GameObject lastWall = wallList[wallList.Count - 2]; // "- 2" porque izq y der alternan (izq - der - izq - der...)
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
