using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private EdgeDetector _edgeDetector;
    [SerializeField] private EnemyRotator _enemyRotator;

    private Vector2 _direction;
    private bool _isMovingRight = true;

    private void Start()
    {
        _direction = gameObject.transform.right;
    }

    private void OnEnable()
    {
        _edgeDetector.OffEdgeDetected += ChangeDirection;
    }

    private void OnDisable()
    {
        _edgeDetector.OffEdgeDetected -= ChangeDirection;
    }

    private void FixedUpdate()
    {
        gameObject.transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void ChangeDirection()
    {
        if (_isMovingRight)
        {
            gameObject.transform.eulerAngles = _enemyRotator.Flip(_isMovingRight);

            _isMovingRight = false;
        }
        else
        {
            gameObject.transform.eulerAngles = _enemyRotator.Flip(_isMovingRight);

            _isMovingRight = true;
        }
    }
}
