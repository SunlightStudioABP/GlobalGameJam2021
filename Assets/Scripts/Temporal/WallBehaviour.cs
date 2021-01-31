using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehaviour : MonoBehaviour
{
    private void OnDestroy()
    {
        Generator gen = GetComponentInParent<Generator>();

        if (gen)
            GetComponentInParent<Generator>().GenerateWall();
    }
}
