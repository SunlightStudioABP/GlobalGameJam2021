using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBehaviour : MonoBehaviour
{
    private void OnDestroy()
    {
        Generator gen = GetComponentInParent<Generator>();

        if (gen)
            GetComponentInParent<Generator>().GenerateGround();
    }
}
