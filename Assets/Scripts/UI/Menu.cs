using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    enum eSceneIndex
    {
        MENU_SCENE = 0,
        KICKER_SCENE,
        GOALKEEPER_SCENE,
        HIGHSCORES_SCENE
    }

    public void MainMenu()
    {
        SceneManager.LoadScene((int)eSceneIndex.MENU_SCENE);
    }

    public void PlayAsKicker()
    {
        PlayerPrefs.SetInt("mode", (int)eSceneIndex.KICKER_SCENE);
        SceneManager.LoadScene((int)eSceneIndex.KICKER_SCENE);
    }

    public void PlayAsGoalKeeper()
    {
        PlayerPrefs.SetInt("mode", (int)eSceneIndex.GOALKEEPER_SCENE);
        SceneManager.LoadScene((int)eSceneIndex.GOALKEEPER_SCENE);
    }

    public void Highscores()
    {
        SceneManager.LoadScene((int)eSceneIndex.HIGHSCORES_SCENE);
    }

    public void PlayAgain()
    {
        if (PlayerPrefs.HasKey("mode"))
        {
            int scenceIndex = PlayerPrefs.GetInt("mode");
            SceneManager.LoadScene(scenceIndex);
        }
        else
        {
            MainMenu();
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
