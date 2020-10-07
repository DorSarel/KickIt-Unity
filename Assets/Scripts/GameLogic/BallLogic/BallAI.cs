using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAI : MonoBehaviour
{
    [SerializeField] private Transform _hands;

    private BallUtils _utils;
    private Vector3 _handsStartPosition;
    private bool _enableKick;
    private const int k_PushValue = 5;
    private const float k_leftBoundary = -6f;
    private const float k_rightBoundary = 3f;
    private const float k_bottomBoundary = -2f;
    private const float k_topBoundary = 2.85f;

    // Start is called before the first frame update
    void Start()
    {
        _handsStartPosition = _hands.position;
        _enableKick = true;

        _utils = transform.GetComponent<BallUtils>();
        if (_utils == null)
        {
            Debug.LogError("Failed to get Utils script");
        }

        GameManager.OnEndGame += EndGame;
    }

    void Update()
    {
        if (_enableKick)
        {
            _enableKick = false;
            float randomZ = Random.RandomRange(k_leftBoundary, k_rightBoundary);
            float randomY = Random.RandomRange(k_bottomBoundary, k_topBoundary);
            _utils.Target = new Vector3(_handsStartPosition.z + k_PushValue, _handsStartPosition.y + randomY, _handsStartPosition.x + randomZ);
            StartCoroutine(ShootBall());
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag.ToLower() == "player")
        {
            _utils.TriggerCollision();
        }

        StartCoroutine(ResetBallPosition());
    }

    void OnDisable()
    {
        GameManager.OnEndGame -= EndGame;
    }

    private void EndGame()
    {
        _utils.MoveBall = false;
        _enableKick = false;
        Destroy(this.gameObject);
    }

    IEnumerator ResetBallPosition()
    {

        transform.position = _utils.StartPosition;
        _utils.MoveBall = false;
        _enableKick = true;
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator ShootBall()
    {
        yield return new WaitForSeconds(1f);
        _utils.PlayShotSound();
        _utils.MoveBall = true;
        _utils.BallPower = 74.5f;
    }
}
