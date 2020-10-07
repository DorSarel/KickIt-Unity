using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallUtils : MonoBehaviour
{
    [SerializeField] private AudioSource _kickBallAudio;
    [SerializeField] private AudioSource _hitTargetAudio;

    public delegate void CollisionAction();
    public static event CollisionAction OnCollision;

    public bool MoveBall {get; set;}
    public float BallPower { get; set; }
    public Vector3 Target { get; set; }
    public Vector3 StartPosition { get; set; }

    void Start()
    {
        StartPosition = transform.position;
        MoveBall = false;
    }

    void FixedUpdate()
    {
        if (MoveBall)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, BallPower * Time.deltaTime);
        }
    }

    public void PlayShotSound()
    {
        _kickBallAudio.Play();
    }

    public void TriggerCollision()
    {
        _hitTargetAudio.Play();
        if (OnCollision != null)
        {
            OnCollision.Invoke();
        }
    }
}
