using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finder : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.1f;
    [SerializeField] private Ability _ability;

    public event Action<Enemy> EnemyFound;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        _ability.DurationStarted += StartLooking;
        _ability.DurationCompleted += StopLooking;
    }

    private void StartLooking(float radius)
    {
        _coroutine = StartCoroutine(LookForEnemy(radius));
    }

    private void StopLooking()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator LookForEnemy(float radius)
    {
        var wait = new WaitForSeconds(_fireRate);

        while (enabled)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);

            float nearestDistance = float.MaxValue;

            Enemy nearestEnemy = null;

            foreach (Collider2D collider in hits)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    float distance = Vector2.Distance(gameObject.transform.position, collider.transform.position);

                    if (distance < nearestDistance)
                    {
                        nearestDistance = distance;

                        nearestEnemy = enemy;
                    }
                }
            }

            if (nearestEnemy != null)
            {
                EnemyFound?.Invoke(nearestEnemy);
            }

            yield return wait;
        }
    }
}
