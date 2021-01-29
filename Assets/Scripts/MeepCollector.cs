using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeepCollector : MonoBehaviour
{

    public static MeepCollector _instance = new MeepCollector();

    List<GameObject> meeps = new List<GameObject>();


    private void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
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

        if(meeps.Count < 2)
        {
            meep.GetComponent<FollowPlayer>().SetTarget(transform);
        }
        else
        {
            meep.GetComponent<FollowPlayer>().SetTarget(meeps[meeps.Count - 2].transform);
        }
        
    }

    public void DestroyMeep(GameObject meep)
    {
        meeps.Remove(meep);
        UpdateFollows();
    }

    private void UpdateFollows()
    {
        for(int i = 0; i < meeps.Count; i++)
        {
            if (meeps.Count == 1)
            {
                meeps[i].GetComponent<FollowPlayer>().SetTarget(transform);
            }
            else
            {
                print("HOLA");
                meeps[i].GetComponent<FollowPlayer>().SetTarget(meeps[meeps.Count - 2].transform);
            }
        }
    }
}
