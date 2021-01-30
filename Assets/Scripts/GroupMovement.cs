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

    private void fixSpeed()
    {
        currentSpeed = targetSpeed;
    }

    private void accelerate()
    {
        if (currentSpeed < targetSpeed)
            currentSpeed += acceleration;
        if (currentSpeed > targetSpeed)
            fixSpeed();
    }

    private void brake()
    {
        if (currentSpeed > targetSpeed)
            currentSpeed += acceleration;
        if (currentSpeed < targetSpeed)
            fixSpeed();
    }

    void Update()
    {
        if (currentSpeed != targetSpeed)
        {
            if (acceleration > 0)
                accelerate();
            else if (acceleration < 0)
                brake();
        }

        transform.Translate(velocity * currentSpeed * Time.deltaTime);
    }
}