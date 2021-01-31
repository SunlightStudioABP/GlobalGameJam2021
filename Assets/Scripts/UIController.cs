using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    [SerializeField]
    private TextMeshProUGUI distanceText, scoreText, damageText, speedText;


    [SerializeField]
    private AudioSource music;

    [SerializeField]
    AudioClip menuSong, ingameSong, inGameintro;

    [SerializeField]
    private GameObject panelMainMenu, panelGame, panelIngame;

    private GameObject activePanel;

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

    private void Update()
    {
        if (!music.isPlaying)
        {
            music.clip = ingameSong;
            music.Play();
        }
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

        music.clip = menuSong;
        music.Play();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        activePanel.SetActive(false);
        activePanel = panelGame;
        activePanel.SetActive(true);

        music.clip = inGameintro;

        music.Play();
    }

    public void SetDistance(int f)
    {
        distanceText.text = f.ToString() + "m";
    }
    public void SetDamage(float damage)
    {
        damageText.text = damage.ToString();
    }
    public void SetSpeed(float speed)
    {
        speedText.text = speed.ToString();
    }

    public void SetScore(float score)
    {
        scoreText.text = score.ToString();
    }
}
