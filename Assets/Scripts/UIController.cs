using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    [SerializeField]
    private TextMeshProUGUI meepsText;


    [SerializeField]
    private GameObject panelMainMenu, panelGame, panelIngame;

    private GameObject activePanel;

    int meeps;

    private void Awake()
    {
        Time.timeScale = 0;

        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        activePanel = panelMainMenu;    
    }

    public void PauseGame()
    {
        activePanel.SetActive(false);
        activePanel = panelIngame;
        activePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void EndGame()
    {
        activePanel.SetActive(false);
        activePanel = panelMainMenu;
        activePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        activePanel.SetActive(false);
        activePanel = panelGame;
        activePanel.SetActive(true);
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
