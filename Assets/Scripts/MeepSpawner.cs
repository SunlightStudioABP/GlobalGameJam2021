using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meep;

    [SerializeField]
    private Transform parentGroup;

    private void Start()
    {
        InvokeRepeating("Create", 3f, 7f);
    }

    private void Create()
    {
        GameObject instance = Instantiate(meep, parentGroup);

        instance.transform.Translate(Random.Range(-10f, 10f), 0f, Random.Range(0f, 10f));
    }
}
