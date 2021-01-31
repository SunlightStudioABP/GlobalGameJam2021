using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayMode : MonoBehaviour
{
    bool replay = false;

    public static ReplayMode _instance = new ReplayMode();

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

    public void SetBool(bool b) { replay = b; }
    public bool IsReplay() { return replay; }
}
