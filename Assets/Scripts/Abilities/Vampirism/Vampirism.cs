using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using Color = UnityEngine.Color;
using UnityEngine;

public class Vampirism : Ability
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _damageRate = 0.1f;

    public override event Action<float, float> ValuesChanged;
    public event Action<float> HealthStolen;

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

        Sprite.enabled = true;

        Coroutine = StartCoroutine(LookForEnemy());

        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Duration));

        Sprite.enabled = false;

        StopCoroutine(Coroutine);

        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Cooldown));

        StopCoroutine(DurationCoroutine);

        ChangeActiveState();
    }

    private IEnumerator LookForEnemy()
    {
        var wait = new WaitForSeconds(_damageRate);

        float radius = Sprite.bounds.extents.x;

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
                SuckHealth(nearestEnemy);
            }

            yield return wait;
        }
    }

    private void SuckHealth(Enemy defender)
    {
        defender.TakeDamage(_damage);

        HealthStolen?.Invoke(_damage);
    }
}
