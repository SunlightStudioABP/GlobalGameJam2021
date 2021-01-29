using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMeep : MonoBehaviour
{
    private void OnMouseDown()
    {
        MeepCollector._instance.DestroyMeep(gameObject);
        Destroy(gameObject);
    }
}
