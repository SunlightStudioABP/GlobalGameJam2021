using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    public float velocidadRebote = 100f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            SoundEffectsManagerController._instance.PlayAttackSound();
            player.AddScore();

            //gameObject.GetComponentInParent<GroupMovement>().currentSpeed = velocidadRebote;
            other.GetComponentInParent<GroupMovement>().currentSpeed = -velocidadRebote;
        }
    }
}
