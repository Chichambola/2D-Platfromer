using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(JumpController))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(AnimationController))]

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private AnimationController _animationController;

    private float _health = 100f;
    
    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _moveController = GetComponent<MoveController>();
        _jumpController = GetComponent<JumpController>();
        _rotator = GetComponent<Rotator>();
        _animationController = GetComponent<AnimationController>();
    }

    private void FixedUpdate()
    {
        if (_inputReader.Direction != 0)
        {
            _animationController.PlayRunAnimation(_inputReader.Direction);
            _moveController.Move(_inputReader.Direction);
            _rotator.Flip(_inputReader.Direction);
        }

        if (_inputReader.GetIsJump() && _groundDetector.IsGround)
            _jumpController.Jump();
    }
}
