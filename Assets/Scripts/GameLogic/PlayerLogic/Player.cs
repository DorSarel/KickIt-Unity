using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Ball _ball;
    [SerializeField] private bool _enableKick = true;

    void Start()
    {
        _enableKick = true;
        GameManager.OnEndGame += EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && _enableKick)
        {
            _enableKick = false;

            RaycastHit hitInfo;
            Ray ray = new Ray(transform.position, transform.forward);

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.rigidbody != null)
                {
                    if (_ball != null)
                    {
                        _ball.ShootBall(hitInfo.point);
                    }
                }
                else
                {
                    EnableKick();
                }
            }
        }
    }

    private void OnDisable()
    {
        GameManager.OnEndGame -= EndGame;
    }

    public void EnableKick()
    {
        _enableKick = true;
    }

    public void EndGame()
    {
        _enableKick = false;
    }
}
