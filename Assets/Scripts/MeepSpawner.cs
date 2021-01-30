using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab;

    void Start()
    {
        InvokeRepeating("Create", 0f, 5f);
    }

    void Create()
    {
        GameObject instance = Instantiate(prefab);

        instance.transform.Translate(Random.Range(-3.5f, 3.5f), 0, 0);
    }
}
