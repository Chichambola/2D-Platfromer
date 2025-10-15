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
        /*float verticalVelocity = Physics2D.gravity.x * Time.deltaTime;
        
        _rigidbody.velocity.Set(verticalVelocity, _rigidbody.velocity.y);
        
        _rigidbody.AddForce(new Vector2(0, _jumpForce));*/
    }
}
