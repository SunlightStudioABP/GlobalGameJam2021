using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepCollector : MonoBehaviour
{
    public ChildrenMovement terrainMovement;
    public ChildrenMovement obstaclesMovement;

    public GameObject meepParent;

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
        meep.GetComponent<Animator>().SetBool("Collected", true);
        meep.transform.SetParent(meepParent.transform);

        AssignMeepTypeAndPlayerStats(meep);

        SoundEffectsManagerController._instance.PlayCollectMeepSound();

        if (meeps.Count < 2)
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
            GetComponent<Animator>().SetBool("Alive", false);
            Destroy(GetComponent<CharacterController>());
            Destroy(GetComponent<PlayerController>());
            Destroy(GetComponentInParent<GroupMovement>());
            Destroy(terrainMovement);
            Destroy(obstaclesMovement);
            UIController._instance.DieScreen();
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
                meeps[i]?.GetComponent<FollowPlayer>()?.SetTarget(transform);
            }
            else
            {
                meeps[i]?.GetComponent<FollowPlayer>()?.SetTarget(meeps[i-1]?.transform);
            }
        }
    }

    public List<GameObject> getMeepList()
    {
        return meeps;
    }
}
