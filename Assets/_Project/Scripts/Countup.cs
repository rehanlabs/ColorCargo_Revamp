using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Countup : MonoBehaviour
{
    double Timer = 0.0;
    public Text guiText;
    public Text guiText2;
    public string nextLevel;
    public WinManager1 winM1;
    public WinManager winM;
    public WinManager4 winM4;
    public WinManager5 winM5;



    // Start is called before the first frame update
    void Start()
    {
        nextLevel=SceneManager.GetActiveScene().name;
        if (nextLevel == "Level-1")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-1", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-2")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-2", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-3")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-3", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-4")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-4", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-5")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-5", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-6")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-6", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-7")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-7", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-8")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-8", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-9")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-9", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-10")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-10", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-11")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-11", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
        else if (nextLevel == "Level-12")
        {
            float bestTime = PlayerPrefs.GetFloat("BestTimeLevel-12", Mathf.Infinity);
            if (bestTime == Mathf.Infinity)
            {
                guiText2.text = "-";
            }
            else
            {
                guiText2.text = bestTime.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nextLevel == "Level-10" || nextLevel == "Level-11" || nextLevel == "Level-12")
        {
            if (!winM5.timestop)
            {
                Timer += Time.deltaTime;
                int seconds = ((int)Timer);
                guiText.text = "" + seconds;
            }
        }
        else if (nextLevel == "Level-7" || nextLevel == "Level-8" || nextLevel == "Level-9")
        {
            if (!winM4.timestop)
            {
                Timer += Time.deltaTime;
                int seconds = ((int)Timer);
                guiText.text = "" + seconds;
            }
        }
        else if (nextLevel == "Level-2" || nextLevel == "Level-3" || nextLevel == "Level-4" || nextLevel == "Level-5" || nextLevel == "Level-6")
        {
            if (!winM.timestop)
            {
                Timer += Time.deltaTime;
                int seconds = ((int)Timer);
                guiText.text = "" + seconds;
            }
        }
        else if (!winM1.timestop)
        {
            Timer += Time.deltaTime;
            int seconds = ((int)Timer);
            guiText.text = "" + seconds;
        }
    }
}
