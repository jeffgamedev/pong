using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInputController : InputController
{
    [SerializeField] [Range(0f, 8f)] private float _accuracyModifier = 2f;
    [SerializeField] [Range(0.1f, 10f)] private float _findTargetCooldownSeconds = 1f;
    [SerializeField] private BallController _ball = null;

    private Vector2 _target;
    private float _nextTargetTime;

    private void Start()
    {
        _target = transform.position;
    }

    private bool BallIsIncoming()
    {
        return _ball != null && _ball.isActiveAndEnabled && ((_ball.transform.position.x < transform.position.x && _ball.RigidBody.velocity.x > 0) ||
               (_ball.transform.position.x > transform.position.x && _ball.RigidBody.velocity.x < 0));
    }

    private Vector2 GetTarget()
    {
        if (Time.time >= _nextTargetTime)
        {
            var targetPosition = transform.position;
            targetPosition.y = _ball.transform.position.y + Random.Range(0f, _accuracyModifier);
            _target = targetPosition;
            _nextTargetTime = Time.time + _findTargetCooldownSeconds;
        }
        return _target;
    }

    private void Update()
    {
        _upIsHeld = _downIsHeld = false;
        if (BallIsIncoming())
        {
            var targetPosition = GetTarget();
            if (Vector2.Distance(targetPosition, transform.position) > 2f)
            {
                if (targetPosition.y < transform.position.y)
                {
                    _downIsHeld = true;
                }
                else if (targetPosition.y > transform.position.y)
                {
                    _upIsHeld = true;
                }
            }
        }
    }
}
