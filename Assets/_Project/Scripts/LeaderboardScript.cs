using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardScript : MonoBehaviour
{
    public GameObject selectlevelpanel;
    public GameObject howtoplaypanel;
    // Start is called before the first frame update
    void Start()
    {
        selectlevelpanel.SetActive(false);
        howtoplaypanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Leaderboard()
    {
        selectlevelpanel.SetActive(true);
    }
    public void back()
    {
        selectlevelpanel.SetActive(false);
    }
    public void howtoplay()
    {
        howtoplaypanel.SetActive(true);
    }
    public void back2()
    {
        howtoplaypanel.SetActive(false);
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level-2");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level-3");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level-4");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level-5");
    }
    public void Level5()
    {
        SceneManager.LoadScene("Level-6");
    }
    public void Level6()
    {
        SceneManager.LoadScene("Level-7");
    }
    public void Level7()
    {
        SceneManager.LoadScene("Level-8");
    }
    public void Level8()
    {
        SceneManager.LoadScene("Level-9");
    }
    public void Level9()
    {
        SceneManager.LoadScene("Level-10");
    }
    public void Level10()
    {
        SceneManager.LoadScene("Level-11");
    }
    public void Level11()
    {
        SceneManager.LoadScene("Level-12");
    }

}
