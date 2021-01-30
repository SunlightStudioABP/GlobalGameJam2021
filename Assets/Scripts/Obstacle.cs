using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            MeepCollector._instance.KillLastMeep();
        }

        if (other.gameObject.CompareTag("CollectedMeep"))
        {
            MeepCollector._instance.Kill(other.gameObject);
        }
    }
}
