using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenMovement : MonoBehaviour
{
    public float speed = -2f;

    // vector de direccion (eje Z en este caso)
    public Vector3 velocity = new Vector3(0, 0, 1);

    void Update()
    {

        foreach (Transform t in GetComponentsInChildren<Transform>())
        {
            if (t != this.transform)
                t.Translate(velocity * speed * Time.deltaTime);
        }
    }
}
