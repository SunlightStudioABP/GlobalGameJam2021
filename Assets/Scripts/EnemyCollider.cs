using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    float velocidadRebote;
    private void Start()
    {
        velocidadRebote = 300f;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.AddScore();

            //gameObject.GetComponentInParent<GroupMovement>().currentSpeed = velocidadRebote;
            other.GetComponentInParent<GroupMovement>().currentSpeed = -velocidadRebote;
        }
    }
}
