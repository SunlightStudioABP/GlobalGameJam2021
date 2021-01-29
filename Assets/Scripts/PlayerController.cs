using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private CharacterController controller;

    private Vector3 playerVelocity;

    [SerializeField] public int playerMeeps     = 1; //VIDA. Cantidad de Meeps que tenemos.
    [SerializeField] public float playerSpeed   = 2.0f;
    [SerializeField] public float playerDamage  = 0f;


    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move.normalized * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move.normalized;
        }

        controller.Move(playerVelocity * Time.deltaTime);
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    //TODO: Comprobar meeps y ponerle al player sus stats correspondientes
    /* switch ((int) meepCurrentType)
        {
            case 1: //Modo uselessMeep
                
                break;

            case 2: //Modo damageMeep
                break;

            case 3: //modo speedMeep
                break;

        }
    */
}