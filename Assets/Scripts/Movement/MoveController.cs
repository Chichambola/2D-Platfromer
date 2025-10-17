using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private int _pushbackForce;
    [SerializeField, Range(2,6)] private int _pushbackForceMultiplier = 2;

    private Rigidbody2D _rigidbody;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        _rigidbody.velocity = new Vector2(direction * Time.fixedDeltaTime * _moveSpeed, _rigidbody.velocity.y);
    }

    public void Push()
    {
        int tempPushbackForce = _pushbackForce * _pushbackForceMultiplier;

        _rigidbody.AddForce(new Vector2(0, tempPushbackForce));
    }
}