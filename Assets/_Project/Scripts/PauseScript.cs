using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPaused=false;
    public GameObject pausecanvas;
    public GameObject leveluipanel;
    public string currentScene;

    private AudioSource backgroundMusic;
    private AudioSource backgroundMusic1;
    private AudioSource backgroundMusic2;

    void Start()
    {
        pausecanvas.SetActive(false);
        currentScene= SceneManager.GetActiveScene().name;

        backgroundMusic = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        backgroundMusic1 = GameObject.FindGameObjectWithTag("BGM2").GetComponent<AudioSource>();
        backgroundMusic2 = GameObject.FindGameObjectWithTag("BGM3").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    public void Pause()
    {
        
        if (!isPaused)
        {
            leveluipanel.SetActive(false);
            Time.timeScale = 0.0f;
            isPaused = true;
            pausecanvas.SetActive(true);

            if (backgroundMusic != null)
            {
                backgroundMusic.Pause();
            }
            if (backgroundMusic1 != null)
            {
                backgroundMusic1.Pause();
            }
            if (backgroundMusic2 != null)
            {
                backgroundMusic2.Pause();
            }
        }
    }
    public void Resume()
    {
        Time.timeScale = 1.0f;
        isPaused = false;
        pausecanvas.SetActive(false);

        if (backgroundMusic != null)
        {
            backgroundMusic.UnPause();
        }
        if (backgroundMusic1 != null)
        {
            backgroundMusic1.UnPause();
        }
        if (backgroundMusic2 != null)
        {
            backgroundMusic2.UnPause();
        }
    }
    public void MainMenu()
    {
        Time.timeScale = 1.0f;
        NextLevelLoader.isEndless = false;
        NextLevelLoader.levelCount = 12;
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        Time.timeScale=1.0f;
        SceneManager.LoadScene(currentScene);
    }
}
