using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MoveController), typeof(Rotator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EdgeDetector _edgeDetector;
    [SerializeField] private Rotator _rotator;
    
    private MoveController _moveController;
    
    private const int MoveLeft = -1;
    private const int MoveRight = 1;
    private float _direction = MoveRight;

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
    }
    
    private void OnEnable()
    {
        _edgeDetector.OffEdgeDetected += ChangeDirection;
    }

    private void OnDisable()
    {
        _edgeDetector.OffEdgeDetected -= ChangeDirection;
    }

    private void Update()
    {
        _moveController.Move(_direction);
    }

    private void ChangeDirection()
    {
        if (_direction == MoveRight)
        {
            _direction = MoveLeft;
            _rotator.Flip(MoveLeft);
        }
        else if (_direction == MoveLeft)
        {
            _direction = MoveRight;
            _rotator.Flip(MoveRight);
        }
    }
}
