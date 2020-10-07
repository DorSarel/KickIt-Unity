using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private float _movingSpeed = 7f;
    private float _maxSpeed = 17f;

    private float _xPosition;
    private float _topBoundary = 13.0f;
    private float _bottomBoundary = 8.3f;
    private float _leftBoundary = -3.75f;
    private float _rightBoundary = -27.0f;
    private Vector3 _moveTo = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        _xPosition = transform.position.x;
        transform.position = new Vector3(_xPosition, 10.5f, -16.5f);

        BallUtils.OnCollision += IncreaseMoveSpeed;
        GameManager.OnEndGame += EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        float zValue = Random.Range(_leftBoundary, _rightBoundary);
        float yValue = Random.Range(_bottomBoundary, _topBoundary);

        if (_moveTo.x == 0)
        {
            // Need to update the position
            _moveTo = new Vector3(_xPosition, yValue, zValue);
        }

        if (Vector3.Distance(transform.position, _moveTo) < 0.001f)
        {
            // target arrived to desired position
            _moveTo.x = 0;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveTo, _movingSpeed * Time.deltaTime);
        }
    }

    void OnDisable()
    {
        BallUtils.OnCollision -= IncreaseMoveSpeed;
        GameManager.OnEndGame -= EndGame;
    }

    public void EndGame()
    {
        Destroy(this.gameObject);
    }

    public void IncreaseMoveSpeed()
    {
        if (_movingSpeed < _maxSpeed)
        {
            _movingSpeed++;
        }
    }
}
