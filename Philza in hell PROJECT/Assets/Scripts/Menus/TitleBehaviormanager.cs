using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleBehaviormanager : MonoBehaviour
{


    public GameObject levelSelect;
    public GameObject startScreen;

    public bool levelScreen;


    private void Update()
    {
        if (levelScreen == true)
        {
            levelSelect.SetActive(true);
            startScreen.SetActive(false);
        }
        else
        {
            levelSelect.SetActive(false);
            startScreen.SetActive(true);
        }
    }


    public void startGame()
    {
        print("GameScene Loaded!");
        SceneManager.LoadScene("StartScene");
    }

    public void Level1Load()
    {
        print("Level1 Loaded!");
        SceneManager.LoadScene("Level1");
    }

    public void Level2Load()
    {
        print("Level2 Loaded!");
        SceneManager.LoadScene("Level2");
    }


    public void showTitle()
    {
        levelScreen = false;
    }

    public void showLevel()
    {
        levelScreen = true;
    }

}
