using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeed : MonoBehaviour
{
    // velocidad a la que nos moveremos
    // valor modificable desde otros scripts
    public float speed = 0f;

    // vector de direccion (eje Z en este caso)
    private Vector3 velocity = new Vector3(0, 0, 1);

    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }
}