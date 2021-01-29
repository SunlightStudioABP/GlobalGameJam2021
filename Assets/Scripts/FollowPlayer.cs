using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    [SerializeField]
    private float distancia;

    private float speed;

    private void Start()
    {
        speed = target.gameObject.GetComponent<PlayerMovement>().GetSpeed();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        if((target.position - transform.position).magnitude > distancia)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
