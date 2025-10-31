using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private BulletMover _mover;

    private int _lifespan;
    private int lifeDelay = 1;
    private int _lifespanMin = 3;
    private int _lifespanMax = 5;
    private Coroutine _followCoroutine;
    private Coroutine _agingCoroutine;

    private void OnEnable()
    {
        _lifespan = Random.Range(_lifespanMin, _lifespanMax);

        if (_followCoroutine != null)
            StopCoroutine(_followCoroutine);

        if (_agingCoroutine != null)
            StopCoroutine(_agingCoroutine);
    }

    public void FollowTarget(Enemy enemy)
    {
        _agingCoroutine = StartCoroutine(AgingRoutine());

        _followCoroutine = StartCoroutine(FollowRoutine(enemy));
    }

    private IEnumerator FollowRoutine(Enemy enemy)
    {
        while (enabled)
        {
            if (enemy != null)
                _mover.Move(enemy);
            else
                Destroy(gameObject);

            yield return null;
        }
    }

    private IEnumerator AgingRoutine()
    {
        var wait = new WaitForSecondsRealtime(lifeDelay);

        int currentLife = 0;

        while (currentLife != _lifespan)
        {
            currentLife++;

            yield return wait;
        }

        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Enemy enemy))
        {
            enemy.TakeDamage(_damage);

            Destroy(gameObject);
        }
    }
}
