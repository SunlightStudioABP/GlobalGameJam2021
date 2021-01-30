using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 playerVelocity;

    public int   playerMeeps, playerMeepsDamageType; // playerMeeps --> VIDA. Cantidad de Meeps que tenemos
                                                     // playerMeepsDamageType --> Cantidad de Meeps de tipo danyo que tenemos

    public float playerDamage, playerSpeed; //Danyo y velocidad que tiene el player

    public int   playerScore, playerScoreDistance; //Puntuaciones.
                                                   //PlayerScore -> Puntos acumulados por ir haciendole danyo al boss
                                                   //PlayerScoreDistance -> Distancia recorrida (en "metros") durante la partida
    public float scoreMultiplicator;
    [SerializeField] float scoreMultiplicatorRatio;

    public float groupSpeed;  // Velocidad a la que se mueve el GRUPO

    private void Start()
    {
        playerMeeps = 1;
        playerMeepsDamageType = 0;
        playerDamage = 0f;
        playerSpeed = 5f;
        groupSpeed = 0f;

        scoreMultiplicator = 0f;
        scoreMultiplicatorRatio = 0.1f; //Aumenta el multiplicador 0.1 cada X segundos

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

    public void UpdatePlayerSpecsAfterAddingNewMeep(int meepType) //Actualizamos las estadisticas base del player cuando anyadimos un meet nuevo. Lo llamamos desde MeepCollector
    {
        switch (meepType)
        {
            case 0: // Modo damageMeep, subimos el danyo
                playerDamage++;
                InvokeRepeating("UpdatePlayerSpecsByMeepSurviving", 5f, 5f);
                break;

            case 1: // Modo speedMeep, subimos la velocidad
              //  playerSpeed++;
                GetComponentInParent<GroupMovement>().targetSpeed = 10f;
                break;
        }
    }

    public void UpdatePlayerSpecsByMeepSurviving() //Actualizamos los stats nos aporta cada meep en funcion del tiempo que lleven sobreviviendo (recompensa)
    {
       List<GameObject> meeps =  MeepCollector._instance.getMeepList();

        for(int i = 0; i<meeps.Count; i++)
        {
            if((int)meeps[i].GetComponent<MeepController>().getMeepCurrentType() == 1) //Averiguamos si el meep es de tipo danyo
            {
                playerMeepsDamageType++;
                scoreMultiplicator += scoreMultiplicatorRatio;
                //Debug.Log(scoreMultiplicator);
            }
        }

        playerDamage += scoreMultiplicator;
        Debug.Log(playerDamage);

    }



}