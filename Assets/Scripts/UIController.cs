﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    private GameObject panelMainMenu, panelGame, panelIngame, panelGameOver, panelCredits;
    int actualOption = 1, pauseOption = 1, gameOverOption = 1;

    [SerializeField]
    private Sprite selectedPlay, selectedCredits, selectedExit, selectedMainMenu, selectedResume, selectedPlayAgain, selectedGameOverMainMenu;

    [SerializeField]
    private Image mainMenuImage, pauseMenuImage, gameOverImage;

    public float volume = 0.3f;

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
            if (!ReplayMode.replay)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
                {
                    SoundEffectsManagerController._instance.PlayMenuOptionSound();
                    actualOption--;
                    if (actualOption == 0)
                        actualOption = 3;
                }
                if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                {
                    SoundEffectsManagerController._instance.PlayMenuOptionSound();
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

                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
                {
                    SoundEffectsManagerController._instance.PlaySelectMenuOptionSound();
                    switch (actualOption)
                    {
                        case 1:
                            StartGame();
                            break;
                        case 2:
                            CreditsScreen();
                            break;
                        case 3:
                            Application.Quit();
                            break;
                    }
                }
            }
            else
            {
                StartGame();
            }
            
        }
        #endregion
        #region Pause Menu
        if (activePanel == panelIngame)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                SoundEffectsManagerController._instance.PlayMenuOptionSound();
                pauseOption--;
                if (pauseOption == 0)
                    pauseOption = 2;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundEffectsManagerController._instance.PlayMenuOptionSound();
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

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
            {
                SoundEffectsManagerController._instance.PlaySelectMenuOptionSound();
                switch (pauseOption)
                {
                    case 1:
                        ResumeGame();
                        break;
                    case 2:
                        ReplayMode.replay = false;
                        EndGame();
                        break;
                }
            }
        }
        #endregion
        #region GameOver Menu
        if (activePanel == panelGameOver)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                SoundEffectsManagerController._instance.PlayMenuOptionSound();
                gameOverOption--;
                if (gameOverOption == 0)
                    gameOverOption = 2;
            }
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                SoundEffectsManagerController._instance.PlayMenuOptionSound();
                gameOverOption++;
                if (gameOverOption == 3)
                    gameOverOption = 1;
            }

            switch (gameOverOption)
            {
                case 1:
                    gameOverImage.sprite = selectedPlayAgain;
                    break;
                case 2:
                    gameOverImage.sprite = selectedGameOverMainMenu;
                    break;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                SoundEffectsManagerController._instance.PlaySelectMenuOptionSound();
                switch (gameOverOption)
                {
                    case 1:
                        ReestartGame();
                        break;
                    case 2:
                        EndGame();
                        break;
                }
            }
        }
        #endregion

        #region Credits
        if (activePanel == panelCredits)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
                Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                SoundEffectsManagerController._instance.PlaySelectMenuOptionSound();
                //Provisional
                activePanel.SetActive(false);
                activePanel = panelMainMenu;
                activePanel.SetActive(true);
                music.volume = 1f;
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

    public void DieScreen()
    {
        gameOverOption = 1;
        activePanel.SetActive(false);
        activePanel = panelGameOver;
        activePanel.SetActive(true);
        music.volume = volume;
    }

    public void CreditsScreen()
    {
        activePanel.SetActive(false);
        activePanel = panelCredits;
        activePanel.SetActive(true);
        music.volume = volume;
    }

    private void PauseGame()
    {
        pauseOption = 1;
        activePanel.SetActive(false);
        activePanel = panelIngame;
        activePanel.SetActive(true);
        Time.timeScale = 0;
        music.volume = volume;
    }

    public void EndGame()
    {
        ReplayMode.replay = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void ReestartGame()
    {
        ReplayMode.replay = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void StartGame()
    {
        Time.timeScale = 1f;
        activePanel.SetActive(false);
        activePanel = panelGame;
        activePanel.SetActive(true);

        music.clip = inGameintro;

        music.Play();
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
        activePanel.SetActive(false);
        activePanel = panelGame;
        activePanel.SetActive(true);

        music.volume = 1f;
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
