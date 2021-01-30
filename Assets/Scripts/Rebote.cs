using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rebote : MonoBehaviour
{
    [SerializeField]
    private float velocidadRebote;

    private void Start()
    {
        velocidadRebote = -1000f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rebote"))
            GetComponent<GroupMovement>().currentSpeed = velocidadRebote;
    }
}
