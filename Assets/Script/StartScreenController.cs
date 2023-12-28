using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenController : MonoBehaviour
{
    public GameObject LoadScreenUI;
    public GameObject StartScreenUI;

    private AudioSource loadingPopUpSound;


    public float DelayBeforeLoading = 3;

    // Start is called before the first frame update
    void Start()
    {
        loadingPopUpSound = GetComponent<AudioSource>();
    }

    public void StartGame() 
    {
        Time.timeScale = 1f;
        StartCoroutine(Loading());
    }

    IEnumerator Loading()
    {
        yield return new WaitForSeconds(1f);
        StartScreenUI.SetActive(false);
        LoadScreenUI.SetActive(true);
        loadingPopUpSound.Play();

        yield return new WaitForSeconds(DelayBeforeLoading);

        SceneManager.LoadScene("Level_1");
    }

    public void LoadCreditScene()
    {
        SceneManager.LoadScene("Credit");
    }

    public void Exit()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
