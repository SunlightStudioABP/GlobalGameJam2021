using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 playerVelocity;

    public int playerMeeps;     // VIDA. Cantidad de Meeps que tenemos.
    public float playerDamage;
    public float playerSpeed;   // velocidad a la que se mueve el PLAYER
    public float groupSpeed;    // velocidad a la que se mueve el GRUPO

    private void Start()
    {
        playerMeeps = 1;
        playerDamage = 0f;
        playerSpeed = 5f;
        groupSpeed = 0f;

        controller = gameObject.AddComponent<CharacterController>();

        controller.minMoveDistance = 0f;
        controller.skinWidth = 0f;
    }

    void Update()
    {
        // Movimiento de grupo
        GroupMovement groupMovement = GetComponentInParent<GroupMovement>();
        Vector3 parentVelocity = new Vector3();

        // si esta habilitado, actualizamos el valor del vector de velocidad del grupo                      <-----
        if (groupMovement.enabled)
            parentVelocity = groupMovement.velocity * groupMovement.currentSpeed;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(movement.normalized * Time.deltaTime * playerSpeed);

        if (movement != Vector3.zero)
        {
            gameObject.transform.forward = movement.normalized;
        }

        // calculamos el vector velocidad final, sumando la velocidad del grupo a la velocidad del object   <-----
        Vector3 totalVelocity = parentVelocity + playerVelocity;

        controller.Move(totalVelocity * Time.deltaTime);
    }

    public float GetSpeed()
    {
        return playerSpeed;
    }

    public void NewPlayerSpecsByAddingNewMeep(int meepType) //Cambiamos las estadisticas del player cuando anyadimos un meet nuevo
    {
        switch (meepType)
        {
            case 0: // Modo damageMeep, subimos el danyo
                playerDamage++;
                break;

            case 1: // Modo speedMeep, subimos la velocidad
              //  playerSpeed++;
                GetComponentInParent<GroupMovement>().targetSpeed = 10f;
                break;
        }
    }

}