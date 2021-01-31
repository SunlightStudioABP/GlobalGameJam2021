using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform target;
    private float distancia = 2.5f;
    public float speed = 10f;

    private void Update()
    {
        if(target)
        transform?.LookAt(target.transform.position);

        if (target && transform && (target.position - transform.position).magnitude > distancia)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
    public void SetTarget(Transform t)
    {
        target = t;
    }


}
