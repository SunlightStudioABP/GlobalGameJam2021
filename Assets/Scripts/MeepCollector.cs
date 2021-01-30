using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepCollector : MonoBehaviour
{
    /* 
        Resumen: 
            no pongais ningun "new" fuera de ninguna funcion

        Motivo asqueroso que os podeis saltar si os aburre:
            Los "news" en las declaraciones no siguen un orden entre ellos ni tienen por que ejecutarse antes de Awake.
            Lo que me ha pasado es que primero hizo "new" la lista de meeps, despues se ejecuto awake, y despues se ejecuto el
            "new" que marco abajo con una flechita ("<-----"). Entonces se ejecuto primero THIS.meeps = new List, se entro a this.Awake,
            se hizo _instance = this, y DESPUES se ejecuto _instance = new MeepCollector. Esa nueva instancia creada no ejecuta la linea
            de "new List" de meeps, porque las inicializaciones fuera de las funciones se ejecutan una unica vez (al leer Unity el fichero
            por primera vez). Entonces si yo despues quiero operar con la lista de meeps, de forma completamente aleatoria me falla con un
            nullPointerException (o como se llame xD). Al quitarlo, de paso la consola deja de poner warnings cerdos
    */

    public static MeepCollector _instance;   //      <-----

    List<GameObject> meeps;

    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
            meeps = new List<GameObject>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void GetMeep(GameObject meep)
    {
        meeps.Add(meep);
        meep.AddComponent<FollowPlayer>();
        Destroy(meep.GetComponent<MeepPicker>());
        meep.tag = "CollectedMeep";
        AssignMeepTypeAndPlayerStats(meep);

        if(meeps.Count < 2)
        {
            meep.GetComponent<FollowPlayer>().SetTarget(transform);
        }
        else
        {
            meep.GetComponent<FollowPlayer>().SetTarget(meeps[meeps.Count - 2].transform);
        }
        
    }

    public void AssignMeepTypeAndPlayerStats(GameObject meep)
    {
        switch (meeps.Count)
        {
            case 0: //PLAYER. NO ES UN MEEP. NO HACEMOS NADA
                break;

            case 1: //PRIMER Meep de la cola (despues del player). Meep de danyo
                meep.GetComponent<MeepController>().setMeepCurrentType(0); //Asignamos el tipo al meep
                GetComponent<PlayerController>().UpdatePlayerSpecsAfterAddingNewMeep(0);
                break;

            case 2: //SEGUNDO Meep de la cola (despues del player). Meep de velocidad
                meep.GetComponent<MeepController>().setMeepCurrentType(1); //Asignamos el tipo al meep
                GetComponent<PlayerController>().UpdatePlayerSpecsAfterAddingNewMeep(1);
                break;

            case 3: //TERCER Meep de la cola (despues del player). Meep de danyo
                meep.GetComponent<MeepController>().setMeepCurrentType(0); //Asignamos el tipo al meep
                GetComponent<PlayerController>().UpdatePlayerSpecsAfterAddingNewMeep(0);
                break;

            case 4: //CUARTO Meep de la cola (despues del player). Meep de velocidad
                meep.GetComponent<MeepController>().setMeepCurrentType(1); //Asignamos el tipo al meep
                GetComponent<PlayerController>().UpdatePlayerSpecsAfterAddingNewMeep(1);
                break;

            case 5: //QUINTO Meep de la cola (despues del player). Meep de danyo
                meep.GetComponent<MeepController>().setMeepCurrentType(0); //Asignamos el tipo al meep
                GetComponent<PlayerController>().UpdatePlayerSpecsAfterAddingNewMeep(0);
                break;
        }
    }

    public void DestroyMeep(GameObject meep)
    {
        meeps.Remove(meep);
        Destroy(meep);
        UpdateFollows();
    }

    public void KillLastMeep()
    {
        if(meeps.Count > 0)
        {
            DestroyMeep(meeps[meeps.Count - 1]);
        }
        else
        {
            UIController._instance.EndGame();
        }
    }

   public void Kill(GameObject meep)
   {
        DestroyMeep(meep);
   }

    private void UpdateFollows()
    {
        for(int i = meeps.Count - 1; i >= 0; i--)
        {
            if(i == 0)
            {
                meeps[i].GetComponent<FollowPlayer>().SetTarget(transform);
            }
            else
            {
                meeps[i].GetComponent<FollowPlayer>().SetTarget(meeps[i-1].transform);
            }
        }
    }

    public List<GameObject> getMeepList()
    {
        return meeps;
    }
}
