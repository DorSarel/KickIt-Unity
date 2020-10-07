using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Player _player;
    private BallUtils _utils;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindObjectOfType<Player>();

        if (_player == null)
        {
            Debug.LogError("Failed to find player object");
        }

        _utils = transform.GetComponent<BallUtils>();
        if (_utils == null)
        {
            Debug.LogError("Failed to get Utils script");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "target")
        {
            _utils.TriggerCollision();
        }

        ResetBallPosition();
    }

    public void ShootBall(Vector3 i_Target)
    {
        _utils.MoveBall = true;
        _utils.BallPower = 100f;
        _utils.Target = i_Target;
        _utils.PlayShotSound();
    }

    public void ResetBallPosition()
    {
        transform.position = _utils.StartPosition;
        _utils.MoveBall = false;

        if (_player != null)
        {
            _player.EnableKick();
        }
    }
}
