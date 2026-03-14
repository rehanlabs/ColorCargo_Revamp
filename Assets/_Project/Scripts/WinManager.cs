using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public GameObject firstMovingObject; // Reference to the first moving object
    public GameObject secondMovingObject; // Reference to the second moving object

    public AudioSource newBackgroundMusic;

    public float delayTime = 2f; // Delay time in seconds

    private bool firstObjectStartedMoving = false;
    private bool secondObjectStartedMoving = false;
    private bool delayStarted = false;

    public GameObject levelWinPanel;

    private float bestTime=Mathf.Infinity;
    public Text guiText;
    public Text guiText2;

    private string nextLevel;
    public bool timestop=false;

    // Update is called once per frame
    void Start()
    {
        levelWinPanel.SetActive(false);
    }
    void Update()
    {
        // Check if the first moving object has started moving
        if (!firstObjectStartedMoving && firstMovingObject != null && firstMovingObject.GetComponent<Train>().IsMovementStarted())
        {
            firstObjectStartedMoving = true;
        }

        // Check if the second moving object has started moving
        if (!secondObjectStartedMoving && secondMovingObject != null && secondMovingObject.GetComponent<Train>().IsMovementStarted())
        {
            secondObjectStartedMoving = true;
        }

        // Check if both objects have started moving
        if (firstObjectStartedMoving && secondObjectStartedMoving && !delayStarted)
        {
            // Start the delay timer
            delayStarted = true;
            StartCoroutine(DelayAndLoadScene());
        }
    }

    // Coroutine for delaying and loading the scene
    private IEnumerator DelayAndLoadScene()
    {
        timestop = true;
        yield return new WaitForSeconds(delayTime);

        // Load the "MainMenu" scene
        
        // Calculate the current level completion time
        float currentTime = float.Parse(guiText.text);
        nextLevel = SceneManager.GetActiveScene().name;
        if (nextLevel == "Level-1")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-1", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-1", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-2")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-2", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-2", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-3")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-3", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-3", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-4")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-4", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-4", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-5")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-5", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-5", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-6")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-6", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-6", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-7")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-7", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-7", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-8")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-8", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-8", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-9")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-9", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-9", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-10")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-10", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-10", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-11")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-11", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-11", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }
        else if (nextLevel == "Level-12")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-12", Mathf.Infinity);
            // Compare with the best time
            if (currentTime < bestTime)
            {
                bestTime = currentTime;
                PlayerPrefs.SetFloat("BestTimeLevel-12", bestTime); // Save the best time

                // Load the "MainMenu" scene

            }
        }



        AudioSource previousBackgroundMusic = GameObject.FindGameObjectWithTag("BGM").GetComponent<AudioSource>();
        if (previousBackgroundMusic != null)
        {
            previousBackgroundMusic.Stop();
        }

        AudioSource bgm2 = GameObject.FindGameObjectWithTag("BGM2").GetComponent<AudioSource>();
        if (bgm2 != null)
        {
            bgm2.Stop();
        }

        AudioSource bgm3 = GameObject.FindGameObjectWithTag("BGM3").GetComponent<AudioSource>();
        if (bgm3 != null)
        {
            bgm3.Stop();
        }


        // Play the new background music
        if (newBackgroundMusic != null)
        {
            newBackgroundMusic.Play();
        }

        levelWinPanel.SetActive(true);
    }
}
