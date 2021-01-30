using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    private CharacterController controller;

    private Vector3 playerVelocity;

    [SerializeField] public int playerMeeps; //VIDA. Cantidad de Meeps que tenemos.
    [SerializeField] public float playerSpeed;  
    [SerializeField] public float playerDamage;


    private void Start()
    {
        playerMeeps = 1;
        playerSpeed = 5.0f;
        playerDamage = 0f;

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

    public void NewPlayerSpecsByAddingNewMeep(int meepType) //Cambiamos las estadisticas del player cuando anyadimos un meet nuevo
    {
        switch (meepType)
        {
            case 0: //Modo damageMeep, subimos el danyo
                playerDamage++;
                break;

            case 1: //Modo speedMeep, subimos la velocidad
                playerSpeed++;
                break;
        }
    }

}