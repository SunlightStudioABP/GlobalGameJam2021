using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public GameObject groundPrefab;

    private List<GameObject> ground;
    private float depthMagnitude;

    private const int groundSize = 3;

    private void Awake()
    {
        depthMagnitude = groundPrefab.transform.localScale.z;
        ground = new List<GameObject>(groundSize);

        for (int i = 0; i < groundSize; ++i)
        {
            GameObject instance = Instantiate(groundPrefab, transform);
            instance.transform.Translate(new Vector3(0, 0, i * depthMagnitude));

            ground.Add(instance);
        }
    }

    public void GenerateGround()
    {
        Vector3 lastItemPosition = ground[ground.Count - 1].transform.position;
        Vector3 translation = lastItemPosition + new Vector3(0, 0, depthMagnitude);

        GameObject instance = Instantiate(groundPrefab, transform);
        instance.transform.Translate(translation);

        ground.Add(instance);
    }
}
