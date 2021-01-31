using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepPicker : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            MeepCollector._instance.GetMeep(gameObject);
    }
}
