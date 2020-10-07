using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _timerCounter = 30f;

    public delegate void TimerAction(float timeToPlay);
    public static event TimerAction OnTimerChange;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RunTimer());
    }

    IEnumerator RunTimer()
    {
        while (_timerCounter > 0)
        {
            _timerCounter--;
            OnTimerChange.Invoke(_timerCounter);
            yield return new WaitForSeconds(1f);
        }

        _gameManager.TriggerEngGameEvents();
    }
}
