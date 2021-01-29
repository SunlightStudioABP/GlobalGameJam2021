using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepPicker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
            MeepCollector._instance.GetMeep(this.gameObject);
    }
}
