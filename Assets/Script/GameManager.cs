using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject GameOverUI;
    public GameObject WinScreenUI;
    public GameObject LoadScreenUI;
    public GameObject PauseMenuUI;

    private AudioSource loadingPopUpSound;
    private bool isGamePaused = false;
    // Update is called once per frame

    private void Start()
    {
        Time.timeScale = 1f;
        loadingPopUpSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (GameOverUI != null && GameOverUI.activeSelf)
        {
            return;
        }

        if (WinScreenUI != null && WinScreenUI.activeSelf)
        {
            return;
        }

        HandleInput();

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameOver();
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            WinGame();
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isGamePaused)
        {
            PauseGame();
        }
    }

    void WinGame ()
    {

        if (WinScreenUI != null)
        {
            WinScreenUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void GameOver()
    {
        if (GameOverUI != null)
        {
            GameOverUI.SetActive(true);

            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        Debug.Log("Loading coroutine started");
        Time.timeScale = 1f;

        WinScreenUI.SetActive(false);
        LoadScreenUI.SetActive(true);
        loadingPopUpSound.Play();

        Debug.Log("Waiting for sound to finish");
        yield return null;

        Debug.Log("Loading next scene");
        SceneManager.LoadScene("Level_2");

        Time.timeScale = 1f;
        Debug.Log("Loading coroutine finished");
    }

    public void PauseGame()
    {
        isGamePaused = true;
        Time.timeScale = 0f;
        PauseMenuUI.SetActive(true);
    }

    public void ResumeGame()
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
    }
}
