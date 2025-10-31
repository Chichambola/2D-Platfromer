using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : Ability
{
    [SerializeField] private float _lookForRate = 0.5f;
    [SerializeField] private float _lookForRadius = 15f;
    [SerializeField] private Bullet _bullet;

    public override event Action<float, float> ValuesChanged;

    public override void UseAbility()
    {
        StartCoroutine(PlayCoroutines());
    }

    protected override IEnumerator DurationRoutine(float duration)
    {
        var wait = new WaitForSeconds(DurationDelay);

        while (CurrentDuration != duration)
        {
            CurrentDuration += DurationDelay;

            ValuesChanged?.Invoke(CurrentDuration, duration);

            yield return wait;
        }

        CurrentDuration = 0;
    }

    private IEnumerator PlayCoroutines()
    {
        ChangeActiveState();

        Coroutine = StartCoroutine(LookForEnemy());

        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Duration));

        StopCoroutine(Coroutine);

        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Cooldown));

        StopCoroutine(DurationCoroutine);

        ChangeActiveState();
    }

    private IEnumerator LookForEnemy()
    {
        var wait = new WaitForSeconds(_lookForRate);

        while (enabled)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, _lookForRadius);

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
                ShootBullet(nearestEnemy);
            }

            yield return wait;
        }
    }

    private void ShootBullet(Enemy enemy)
    {
        Bullet bullet = Instantiate(_bullet);

        bullet.transform.position = gameObject.transform.position;

        bullet.FollowTarget(enemy);
    }
}
