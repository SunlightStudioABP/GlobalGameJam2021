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

    private List<GameObject> leftWallList;
    private List<GameObject> rightWallList;
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
        leftWallList = new List<GameObject>();
        rightWallList = new List<GameObject>();
        // rellenar la lista de muros
        for (int i = 0; i < numMaxWallsPerSide; ++i)
        {
            int prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));
            leftWallList.Add(Instantiate(wallPrefabs[prefabRandomIndex], transform)); // izq

            prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));
            rightWallList.Add(Instantiate(wallPrefabs[prefabRandomIndex], transform)); // der
        }

        // los colocamos en su lugar

        // el primer muro no depende de nadie, tenemos que poner su valor inicial
        if (leftWallList.Count > 0)
            leftWallList[0].transform.position = firstLeftWallPos;
        if (rightWallList.Count > 0)
            rightWallList[0].transform.position = firstRightWallPos;

        for (int i = 1; i < leftWallList.Count; ++i)
        {
            GameObject lastWall = leftWallList[i - 1];
            GameObject thisWall = leftWallList[i];

            Bounds lastWallBounds = lastWall.GetComponent<Collider>().bounds;
            Bounds thisWallBounds = thisWall.GetComponent<Collider>().bounds;

            float newX = lastWall.transform.position.x;
            float newY = lastWall.transform.position.y;
            float newZ = lastWall.transform.position.z + Mathf.Abs(lastWallBounds.max.z) + Mathf.Abs(thisWallBounds.min.z);

            thisWall.transform.position = new Vector3(newX, newY, newZ);

            lastWall = rightWallList[i - 1];
            thisWall = rightWallList[i];

            lastWallBounds = lastWall.GetComponent<Collider>().bounds;
            thisWallBounds = thisWall.GetComponent<Collider>().bounds;

            newX = lastWall.transform.position.x;
            newY = lastWall.transform.position.y;
            newZ = lastWall.transform.position.z + Mathf.Abs(lastWallBounds.max.z) + Mathf.Abs(thisWallBounds.min.z);

            thisWall.transform.position = new Vector3(newX, newY, newZ);
        }
    }

    public void GenerateGround()
    {
  /*      Vector3 lastItemPosition = groundList[groundList.Count - 1].transform.position;
        Vector3 translation = lastItemPosition + new Vector3(0, 0, groundDepthMagnitude);

        GameObject instance = Instantiate(groundPrefab, transform);
        instance.transform.Translate(translation);

        groundList.Add(instance);
        */
        GameObject lastGround = groundList[groundList.Count - 1];
        GameObject newGround = Instantiate(groundPrefab, transform);

        Bounds lastGroundBounds = lastGround.GetComponent<Collider>().bounds;
        Bounds newGroundBounds = newGround.GetComponent<Collider>().bounds;

        float newX = lastGround.transform.position.x;
        float newY = lastGround.transform.position.y;
        float newZ = lastGround.transform.position.z + groundDepthMagnitude;

        newGround.transform.position = new Vector3(newX, newY, newZ);

        groundList.Add(newGround);
    }

    public void GenerateWall(float leftRightValue)
    {
        int prefabRandomIndex = (int)Mathf.Round(Random.Range(0, wallPrefabs.Length));

        if (leftRightValue < 0) {
            GameObject lastWall = leftWallList[leftWallList.Count - 1];
            GameObject newWall = Instantiate(wallPrefabs[prefabRandomIndex], transform);

            Bounds lastWallBounds = lastWall.GetComponent<Collider>().bounds;
            Bounds newWallBounds = newWall.GetComponent<Collider>().bounds;

            float newX = lastWall.transform.position.x;
            float newY = lastWall.transform.position.y;
            float newZ = lastWallBounds.center.z + (lastWallBounds.size.z + newWallBounds.size.z) / 2f;

            newWall.transform.position = new Vector3(newX, newY, newZ);

            leftWallList.Add(newWall);
        }
        else if (leftRightValue > 0)
        {
            GameObject lastWall = rightWallList[rightWallList.Count - 1];
            GameObject newWall = Instantiate(wallPrefabs[prefabRandomIndex], transform);

            Bounds lastWallBounds = lastWall.GetComponent<Collider>().bounds;
            Bounds newWallBounds = newWall.GetComponent<Collider>().bounds;

            float newX = lastWall.transform.position.x;
            float newY = lastWall.transform.position.y;
            float newZ = lastWallBounds.center.z + (lastWallBounds.size.z + newWallBounds.size.z) / 2f;

            newWall.transform.position = new Vector3(newX, newY, newZ);

            rightWallList.Add(newWall);
        }
    }
}
