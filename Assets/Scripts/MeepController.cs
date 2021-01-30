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
    }

    public void setMeepCurrentType(int newType)
    {
        switch (newType)
        {
            case 0: //Tipo damageMeep
                meepCurrentType = meepType.damageMeep;
                break;

            case 1: //Tipo speedMeep
                meepCurrentType = meepType.speedMeep;
                break;
        }
    }

    public meepType getMeepCurrentType()
    {
        return meepCurrentType;
    }


}
