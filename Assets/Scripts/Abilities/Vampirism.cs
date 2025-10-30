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
    [SerializeField] private float _damageRate = 0.5f;

    public override event Action<float, float> ValuesChanged;

    public override void UseAbility()
    {
        DurationCoroutine = StartCoroutine(DurationRoutine(Duration));

        RadiusSprite.enabled = true;
    }

    protected override IEnumerator DurationRoutine(float duration)
    {
        var wait = new WaitForSeconds(DurationDelay);

        float currentDuration = 0;

        while (currentDuration != duration)
        {
            if (DurationCoroutine != null && AbilityCoroutine == null)
            {
                AbilityCoroutine = StartCoroutine(LookForEnemy());
            }

            currentDuration += DurationDelay;

            ValuesChanged?.Invoke(currentDuration, duration);

            yield return wait;
        }

        ToggleDurationCoroutines();
    }

    private IEnumerator LookForEnemy()
    {
        var wait = new WaitForSeconds(_damageRate);

        float radius = RadiusSprite.bounds.extents.x;

        while (DurationCoroutine != null)
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

        AbilityCoroutine = TurnOffCoroutine(AbilityCoroutine);
    }

    private void SuckHealth(Enemy defender)
    {
        if (gameObject.TryGetComponent(out Player player))
        {
            defender.TakeDamage(_damage);

            player.Heal(_damage);
        }
    }

    private void ToggleDurationCoroutines()
    {
        if (DurationCoroutine != null)
        {
            RadiusSprite.enabled = false;

            DurationCoroutine = TurnOffCoroutine(DurationCoroutine);

            CooldownCoroutine = StartCoroutine(DurationRoutine(Cooldown));
        }
        else
        {
            CooldownCoroutine = TurnOffCoroutine(CooldownCoroutine);
        }
    }

    private Coroutine TurnOffCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);

        return null;
    }
}
