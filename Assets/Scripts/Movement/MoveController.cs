using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rotator _rotator;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * Time.fixedDeltaTime * _moveSpeed, _rigidbody.velocity.y);
    }
}