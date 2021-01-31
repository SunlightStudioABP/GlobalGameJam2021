using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private int distance;

    public float increaseMovement = 5f;

    [SerializeField]
    private ChildrenMovement[] terreno;

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

    private float score;

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

    private void Idle()
    {
        if (!Input.anyKey)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5f);
        }
    }

    void Update()
    {
        Idle();
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

        distance = (int)System.Math.Round((double)(Time.time / 0.5f));
        UIController._instance.SetDistance(distance);


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
                UIController._instance.SetDamage(playerDamage);
                InvokeRepeating("UpdatePlayerSpecsByMeepSurviving", 5f, 5f);
                break;

            case 1: // Modo speedMeep, subimos la velocidad
              //  playerSpeed++;
                GetComponentInParent<GroupMovement>().targetSpeed += increaseMovement;

                for (int i = 0; i < terreno.Length; i++)
                {
                    terreno[i].speed -= increaseMovement;
                }

                UIController._instance.SetSpeed(GetComponentInParent<GroupMovement>().targetSpeed);
                break;
        }
    }

    public void UpdatePlayerSpecsByMeepSurviving() //Actualizamos los stats nos aporta cada meep en funcion del tiempo que lleven sobreviviendo (recompensa)
    {
        List<GameObject> meeps = MeepCollector._instance.getMeepList();                 //Obtenemos la fila de meeps detras del jugador
        scoreMultiplicator = 0;

        for(int i = 0; i < meeps.Count; i++)
        {
            if ((int)meeps[i].GetComponent<MeepController>().getMeepCurrentType() == 1) //Si el meep es de tipo danyo
            {
                meeps[i].GetComponent<MeepController>().AddDamage(scoreMultiplicatorRatio);
                scoreMultiplicator += meeps[i].GetComponent<MeepController>().GetDamageMultiplier();
            }
        }

        playerDamage = (float)System.Math.Round(scoreMultiplicator, 1);                                             //Sumamos el multiplicador al daño del personaje
        UIController._instance.SetDamage(playerDamage);
        //Debug.Log(playerDamage);
    }

    public void AddScore()
    {
        score += playerDamage;
        UIController._instance.SetScore(score);
    }

}