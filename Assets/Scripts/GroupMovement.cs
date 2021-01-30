using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupMovement : MonoBehaviour
{
    // velocidad a la que nos moveremos
    // valor modificable desde otros scripts
    public float targetSpeed = 0f;
    public float currentSpeed = 0f;
    public float acceleration = 2f;

    // vector de direccion (eje Z en este caso)
    public Vector3 velocity = new Vector3(0, 0, 1);

    void Update()
    {
        if (System.Math.Abs(currentSpeed) < System.Math.Abs(targetSpeed))
            currentSpeed += acceleration;

        transform.Translate(velocity * currentSpeed * Time.deltaTime);
    }
}