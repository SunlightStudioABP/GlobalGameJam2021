using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _instance = new UIController();

    [SerializeField]
    private Text meepsText;

    int meeps;

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

    public void SetMeeps(int n)
    {
        meeps = n;
        UpdateUI();
    }

    private void UpdateUI()
    {
        meepsText.text = meeps.ToString();
    }
}
