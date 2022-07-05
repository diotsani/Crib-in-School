using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [SerializeField]private SoundManager buttonSfx;
    public GameObject pausePanel;

    public bool gameIsPaused = false;

    private void Awake()
    {
        buttonSfx= FindObjectOfType<SoundManager>();
        pausePanel.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        buttonSfx.buttonclickMethod();
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }

    public void Resume()
    {
        buttonSfx.buttonclickMethod();
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void Restart()
    {
        buttonSfx.buttonclickMethod();
        SceneManager.LoadScene("Level 1");
    }

    public void Quit()
    {
        //Application.Quit();
        buttonSfx.buttonclickMethod();
        SceneManager.LoadScene("1. MainMenu");
    }
}
