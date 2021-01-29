using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantSpeed : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;

    private Vector3 velocity = new Vector3(0, 0, 1);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(velocity * speed * Time.deltaTime);
    }

}