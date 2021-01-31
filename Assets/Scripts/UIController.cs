using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController _instance;

    [SerializeField]
    private TextMeshProUGUI distanceText, scoreText, damageText, speedText, distanceTextShadow, scoreTextShadow, damageTextShadow, speedTextShadow;


    [SerializeField]
    private AudioSource music;

    [SerializeField]
    AudioClip menuSong, ingameSong, inGameintro;

    [SerializeField]
    private GameObject panelMainMenu, panelGame, panelIngame;
    int actualOption = 1, pauseOption = 1;

    [SerializeField]
    private Sprite selectedPlay, selectedCredits, selectedExit, selectedMainMenu, selectedResume;

    [SerializeField]
    private Image mainMenuImage, pauseMenuImage;

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

        #region Main Menu
        if (activePanel == panelMainMenu)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                actualOption--;
                if(actualOption == 0)
                    actualOption = 3;
            }
            if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                actualOption++;
                if (actualOption == 4)
                    actualOption = 1;
            }

            switch (actualOption)
            {
                case 1:
                    mainMenuImage.sprite = selectedPlay;
                    break;
                case 2:
                    mainMenuImage.sprite = selectedCredits;
                    break;
                case 3:
                    mainMenuImage.sprite = selectedExit;
                    break;
            }

            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (actualOption)
                {
                    case 1:
                        StartGame();
                        break;
                    case 2:
                        //Creditos
                        break;
                    case 3:
                        Application.Quit();
                        break;
                }
            }
        }
        #endregion
        #region Pause Menu
        if (activePanel == panelIngame)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                pauseOption--;
                if (pauseOption == 0)
                    pauseOption = 2;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                pauseOption++;
                if (pauseOption == 3)
                    pauseOption = 1;
            }

            switch (pauseOption)
            {
                case 1:
                    pauseMenuImage.sprite = selectedResume;
                    break;
                case 2:
                    pauseMenuImage.sprite = selectedMainMenu;
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                switch (pauseOption)
                {
                    case 1:
                        StartGame();
                        break;
                    case 2:
                        EndGame();
                        break;
                }
            }
        }
        #endregion
        #region Game
        if (activePanel == panelGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseGame();
        }
        #endregion

    }

    public void PauseGame()
    {
        pauseOption = 1;
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

        actualOption = 1;
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
        distanceTextShadow.text = f.ToString() + "m";
    }
    public void SetDamage(float damage)
    {
        damageText.text = damage.ToString();
        damageTextShadow.text = damage.ToString();
    }
    public void SetSpeed(float speed)
    {
        speedText.text = speed.ToString();
        speedTextShadow.text = speed.ToString();
    }

    public void SetScore(float score)
    {
        scoreText.text = score.ToString();
        scoreTextShadow.text = score.ToString();
    }
}
