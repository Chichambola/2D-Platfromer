using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MoveController), typeof(Rotator))]
public class Enemy : MonoBehaviour, IAttacker
{
    [SerializeField] private EdgeDetector _edgeDetector;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private Health _health;
    [SerializeField] private float _damage = 15f;

    private const int MoveLeft = -1;
    private const int MoveRight = 1;

    private float _direction = MoveRight;

    private void Awake()
    {
        _moveController = GetComponent<MoveController>();
        _rotator = GetComponent<Rotator>();
    }

    private void OnEnable()
    {
        _edgeDetector.OffEdgeDetected += _rotator.Flip;
    }

    private void OnDisable()
    {
        _edgeDetector.OffEdgeDetected -= _rotator.Flip;
    }

    private void Update()
    {
        _moveController.Move(_direction);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Player player))
        {
            player.TakeDamage(_damage);
        }
    }
    
    public void DealDamage(IAttacker defender)
    {
        defender.TakeDamage(_damage);
    }

    public void TakeDamage(float damage)
    {
        _health.TakeDamage(damage);
        
        if (_health.IsAlive == false)
            Die();
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
