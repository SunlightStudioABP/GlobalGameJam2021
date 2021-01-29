using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField]
    private string tag_to_compare = "";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag_to_compare))
            Destroy(other.gameObject);
    }
}
