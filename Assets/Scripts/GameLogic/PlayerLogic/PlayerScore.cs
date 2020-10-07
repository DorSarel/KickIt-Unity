using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public delegate void ScoreAction(int score);
    public static event ScoreAction OnScoreChange;

    private int _score = 0;
    private int _scoreToAdd = 2;
    private const int k_MaxScoreToAdd = 5;

    // Start is called before the first frame update
    void Start()
    {
        BallUtils.OnCollision += UpdateScore;
        BallUtils.OnCollision += UpdateScoreToAdd;
        GameManager.OnEndGame += EndGame;
    }

    void OnDisable()
    {
        BallUtils.OnCollision -= UpdateScore;
        BallUtils.OnCollision -= UpdateScoreToAdd;
        GameManager.OnEndGame -= EndGame;
    }

    public void UpdateScore()
    {
        _score += _scoreToAdd;
        OnScoreChange(_score);
    }

    public void UpdateScoreToAdd()
    {
        if (_scoreToAdd < k_MaxScoreToAdd)
        {
            _scoreToAdd++;
        }
    }

    public void EndGame()
    {
        GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddHighscoreEntry(_score);
    }
}
