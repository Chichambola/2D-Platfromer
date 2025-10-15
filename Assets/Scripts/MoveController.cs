using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public const string Speed = nameof(Speed);

    [SerializeField] private float _moveSpeed;
    [SerializeField] private Animator _animator;
    [SerializeField] private Rotator _rotator;

    private Rigidbody2D _rigidbody;
    private bool _isMovingRight;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        gameObject.transform.Translate(new Vector2(_moveSpeed * direction * Time.deltaTime, _rigidbody.velocity.y), Space.World);
        
        _animator.SetFloat(Speed, direction);
    }
}