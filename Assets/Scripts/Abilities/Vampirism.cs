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

    public override void UseAbility()
    {
        StartCoroutine(PlayAbility());
    }

    private IEnumerator PlayAbility()
    {
        ToggleActive();

        RadiusSprite.enabled = true;

        AbilityCoroutine = StartCoroutine(LookForEnemy());
        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Duration));

        RadiusSprite.enabled = false;
        StopCoroutine(AbilityCoroutine);

        yield return DurationCoroutine = StartCoroutine(DurationRoutine(Cooldown));

        StopCoroutine(DurationCoroutine);

        ToggleActive();
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

    private IEnumerator LookForEnemy()
    {
        var wait = new WaitForSeconds(_damageRate);

        float radius = RadiusSprite.bounds.extents.x;

        while (enabled)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, radius);

            foreach (Collider2D collider in hits)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    SuckHealth(enemy);
                }
            }

            yield return wait;
        }
    }

    private void SuckHealth(Enemy defender)
    {
        if (gameObject.TryGetComponent(out Player player))
        {
            defender.TakeDamage(_damage);

            player.Heal(_damage);
        }
    }
}
