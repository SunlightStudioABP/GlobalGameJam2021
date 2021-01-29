using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepController : MonoBehaviour
{
    BoxCollider meep_collider;

    public enum meepType { uselessMeep, damageMeep, speedMeep }; // Tipos de MEEPS que van a haber en el juego.
                                                                    // uselessMeep --> Meep para recoger. Sin utilidad
                                                                    // damageMeep --> Meep que anyade danyo al unirse a nuestras filas
                                                                    // speedMeep --> Meep que anyade velocidad al unirse a nuestras filas
    [SerializeField] meepType meepCurrentType;


    private void Awake()
    {
        meepCurrentType =  meepType.uselessMeep;   //por defecto creamos el meep siendo useless
    }

    private void Start()
    {
        meep_collider = gameObject.GetComponent<BoxCollider>();
    }


    private void Update()
    {
       
    }


    private void OnCollisionEnter(Collision collision)
    {
        print("HOLA");

        //if (collision.gameObject.CompareTag("Player"))
          //  print("HOLA");

    }


}
