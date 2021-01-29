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
        //print(meeps.Count);
        UpdateFollows();
    }

    private void UpdateFollows()
    {
        for(int i = meeps.Count - 1; i >= 0; i--)
        {
            print(i);
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
}
