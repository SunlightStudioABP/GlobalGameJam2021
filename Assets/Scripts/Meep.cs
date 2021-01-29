using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meep : MonoBehaviour
{
    BoxCollider meep_collider;

    private void Start()
    {
        meep_collider = gameObject.GetComponent<BoxCollider>();
    }


     void OnCollisionEnter(Collision collision)
    {
        print("HOLA");

        if (collision.gameObject.CompareTag("Player"))
            print("HOLA");

    }


}
