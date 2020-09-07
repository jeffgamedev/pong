using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float DISTANCE_THRESHOLD = 1.25f;
    private const float MINIMUM_SPEED = 20f;

    [SerializeField] private AudioClip _strikeSound = null;
    [SerializeField] private AudioClip _bounceSound = null;
    [SerializeField] private Vector2 _startingPosition = Vector2.zero;
    [SerializeField] [Range(1f, 100f)] private float _speedX = 40f;
    [SerializeField] [Range(1f, 100f)] private float _speedY = 20f;
    [SerializeField] [Range(1f, 100f)] private float _startingSpeed = 20f;
    [SerializeField] private Rigidbody2D _rigidBody = null;

    public Rigidbody2D RigidBody
    {
        get
        {
            return _rigidBody;
        }
    }
    
    private void Start()
    {
        ResetBall(0);
    }

    public void ResetBall(int playerIndex)
    {
        gameObject.SetActive(true);
        transform.position = _startingPosition;
        var velocity = new Vector2(playerIndex == 0 ? _startingSpeed : -_startingSpeed, 0f);
        _rigidBody.velocity = velocity;
    }

    private void  OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Paddle")
        {
            var xMultiplier = other.transform.position.x < transform.position.x ? -1f : 1f;
            var yMultiplier = other.transform.position.y < transform.position.y ? -1f : 1f;
            var distance = Vector2.Distance(transform.position, other.transform.position);
            var velocity = _rigidBody.velocity;
            velocity.x = _speedX * xMultiplier;
            velocity.y = _speedY * yMultiplier * Mathf.Max(0f, distance-DISTANCE_THRESHOLD);
            _rigidBody.velocity = velocity;
            Util.PlayUISound(_strikeSound);
        }
        else
        {
            Util.PlayUISound(_bounceSound);
        }
    }

    private void FixedUpdate()
    {
        if (_rigidBody.velocity.x < MINIMUM_SPEED && _rigidBody.velocity.x > -MINIMUM_SPEED)
        {
            var velocity = _rigidBody.velocity;
            velocity.x *= 2f;
            _rigidBody.velocity = velocity;
        }
    }
}
