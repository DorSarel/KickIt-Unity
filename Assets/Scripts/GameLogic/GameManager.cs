using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public delegate void GameEndAction();
    public static event GameEndAction OnEndGame;

    public void TriggerEngGameEvents()
    {
        if (OnEndGame != null)
        {
            OnEndGame.Invoke();
        }
        StartCoroutine(LoadSceneOnDelay());
    }

    IEnumerator LoadSceneOnDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("HighScore");
    }
}
