using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    private const float STRIKING_DISTANCE = 10f;

    [SerializeField] [Range(10f, 100f)] private float _speed = 25f;
    [SerializeField] private BallController _ball = null;
    [SerializeField] private Rigidbody2D _rigidBody = null;
    [SerializeField] private InputController _input = null;
    [SerializeField] private Animator _animator = null;
    private bool _moving;

    private void FixedUpdate()
    {
        UpdateInput();
    }

    private void Update()
    {
        Animate();
    }

    private void Animate()
    {
        if (_animator == null)
        {
            return;
        }
        _animator.SetBool("moving", _moving);
        var striking = false;
        if (_ball != null && _ball.isActiveAndEnabled)
        {
            striking = Vector2.Distance(transform.position, _ball.transform.position) < STRIKING_DISTANCE;
        }
        _animator.SetBool("striking", striking);
    }

    private void UpdateInput()
    {
        if (_input == null)
        {
            return;
        }
        if (_input.UpIsHeld())
        {
            _moving = true;
            SetVelocityY(_speed);
        }
        else if (_input.DownIsHeld())
        {
            _moving = true;
            SetVelocityY(-_speed);
        }
        else
        {
            _moving = false;
            SetVelocityY(0f);
        }
    }

    private void SetVelocityY(float y)
    {
        var velocity = _rigidBody.velocity;
        velocity.y = y;
        _rigidBody.velocity = velocity;
    }
}
