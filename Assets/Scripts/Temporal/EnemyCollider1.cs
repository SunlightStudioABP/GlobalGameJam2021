using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider1 : MonoBehaviour
{
    public float velocidadRebote = 100f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            player.AddScore();

            // enemigo es empujado
            //gameObject.GetComponentInParent<GroupMovement>().currentSpeed = velocidadRebote;
            // player rebota
            other.GetComponentInParent<GroupMovement>().currentSpeed = -velocidadRebote;
        }
    }
}
