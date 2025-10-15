using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpController : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity.Set(0, _rigidbody.velocity.y);

        _rigidbody.AddForce(new Vector2(0, _jumpForce));
    }
}
