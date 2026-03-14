using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelLoader : MonoBehaviour
{
    public static bool isEndless;
    public static float levelCount = 12;
    public string nextLevel;
    public string[] Levels = new string[11] { "Level-2", "Level-3", "Level-4", "Level-5", "Level-6", "Level-7", "Level-8", "Level-9", "Level-10", "Level-11", "Level-12" };
    public Text levelText;
    
    private void Start()
    {
        nextLevel = SceneManager.GetActiveScene().name;
        UpdateLevelText();
    }
   

    public void NextLevel()
    {
        if (!isEndless)
        {
            if (nextLevel == "Level-1")
            {
                nextLevel = "Level-2";
            }
            else if (nextLevel == "Level-2")
            {
                nextLevel = "Level-3";
            }
            else if (nextLevel == "Level-3")
            {
                nextLevel = "Level-4";
            }
            else if (nextLevel == "Level-4")
            {
                nextLevel = "Level-5";
            }
            else if (nextLevel == "Level-5")
            {
                nextLevel = "Level-6";
            }
            else if (nextLevel == "Level-6")
            {
                nextLevel = "Level-7";
            }
            else if (nextLevel == "Level-7")
            {
                nextLevel = "Level-8";
            }
            else if (nextLevel == "Level-8")
            {
                nextLevel = "Level-9";
            }
            else if (nextLevel == "Level-9")
            {
                nextLevel = "Level-10";
            }
            else if (nextLevel == "Level-10")
            {
                nextLevel = "Level-11";
            }
            else if (nextLevel == "Level-11")
            {
                nextLevel = "Level-12";
            }
            else if (nextLevel == "Level-12")
            {
                int RandomLevel = Random.Range(0, 10);
                nextLevel = Levels[RandomLevel];
                isEndless = true;
                levelCount += 1f;
            }
        }
        else if (isEndless)
        {
            int RandomLevel = Random.Range(0, 10);
            nextLevel = Levels[RandomLevel];
            levelCount += 1f;
            Debug.Log("NextLevel method called");
        }
        
        Debug.Log("in next level");
        UpdateLevelText();
        SceneManager.LoadScene(nextLevel);
    }
    //}
    
    

    

    void UpdateLevelText()
    {
        if (isEndless)
        {
            levelText.text = "Level-" + levelCount;
        }
        else
        {
            int levelNumber;
            if (int.TryParse(nextLevel.Replace("Level-", ""), out levelNumber))
            {
                if (levelText != null)
                {
                    levelText.text = "Level-" + levelNumber;
                }
            }
        }
    }
}
