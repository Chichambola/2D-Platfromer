using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MoveController))]
[RequireComponent(typeof(JumpController))]
[RequireComponent(typeof(GroundDetector))]
[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(AnimationController))]

public class Player : MonoBehaviour, IAttacker
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private Rotator _rotator;
    [SerializeField] private MoveController _moveController;
    [SerializeField] private JumpController _jumpController;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private Collector _collector;

    [SerializeField] private float _health = 100f;
    [SerializeField] private float _damage = 10f;

    private float _maxHealth;
    private int _coinCounter;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _groundDetector = GetComponent<GroundDetector>();
        _moveController = GetComponent<MoveController>();
        _jumpController = GetComponent<JumpController>();
        _rotator = GetComponent<Rotator>();
        _animationController = GetComponent<AnimationController>();
    }

    private void Start()
    {
        _maxHealth = _health;
    }

    private void OnEnable()
    {
        _collector.ItemCollected += IdentifyItem;
    }

    private void OnDisable()
    {
        _collector.ItemCollected -= IdentifyItem;
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

    public void TakeDamage(float damage)
    {
        _health -= damage;

        Debug.Log($"{gameObject.name} υο - {_health}");

        Die();
    }

    public void DealDamage(IAttacker defender)
    {
        defender.TakeDamage(_damage);
    }

    public void Die()
    {
        if (_health <= 0)
        {
            Destroy(this);
        }
    }

    public void PushFromEnemy()
    {
        _moveController.Push();
    }

    private void Heal(Medkit medkit)
    {
        _health += medkit.Heal();

        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    private void IdentifyItem (Item item)
    {
        if(item.TryGetComponent(out Medkit medkit))
        {
            Heal(medkit);
        }
        else if (item.TryGetComponent(out Coin _))
        {
            _coinCounter++;
        }
    }
}