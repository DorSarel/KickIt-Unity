using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _timerText;
    [SerializeField] private Text _endScoreText;
    [SerializeField] private RectTransform _menuCanvas;
    [SerializeField] private RectTransform _endGameCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Timer.OnTimerChange += UpdateTimerText;
        PlayerScore.OnScoreChange += UpdateScore;

        _endGameCanvas.gameObject.SetActive(false);
        GameManager.OnEndGame += ActiveEndGameCanvas;
    }

    void OnDisable()
    {
        Timer.OnTimerChange -= UpdateTimerText;
        PlayerScore.OnScoreChange -= UpdateScore;
    }

    public void UpdateTimerText(float timeToPlay)
    {
        if (timeToPlay < 10f)
        {
            _timerText.color = Color.red;
        }
        _timerText.text = timeToPlay.ToString();
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = string.Format("Score: {0}", score.ToString());
        _endScoreText.text = string.Format("Your game score is {0}", score.ToString());
    }

    // This method is part of the EndGame event
    // For some reason when the scene was loaded for the second time & above, I lost the references for the below UI elements (destroyed...)
    // This caused to NullReferenceException - this is the best work around I was able to find
    public void ActiveEndGameCanvas()
    {
        try
        {
            _menuCanvas = GameObject.Find("MenuCanvas").GetComponent<RectTransform>();
            _endGameCanvas = GameObject.Find("UI").transform.GetChild(2).GetComponent<RectTransform>();

            _menuCanvas.gameObject.SetActive(false);
            _endGameCanvas.gameObject.SetActive(true);
        }
        catch (System.NullReferenceException ex)
        {
            // DO NOTHING
        }
    }
}
