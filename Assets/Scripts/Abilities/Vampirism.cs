using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine;
using Color = UnityEngine.Color;

public class Vampirism : Ability
{
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _damageRate = 0.5f;

    public override void UseAbility()
    {
        RadiusSprite.enabled = true;

        DurationCoroutine = StartCoroutine(DurationRoutine(Duration));
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

            Debug.Log(currentDuration);

            yield return wait;
        }

        ToggleDurationCoroutines();

        yield return null;
    }

    private void ToggleDurationCoroutines()
    {
        if (DurationCoroutine != null)
        {
            StopCoroutine(DurationCoroutine);

            DurationCoroutine = null;

            RadiusSprite.enabled = false;

            CooldownCoroutine = StartCoroutine(DurationRoutine(Cooldown));
        }
        else
        {
            StopCoroutine(CooldownCoroutine);

            CooldownCoroutine = null;
        }
    }

    private IEnumerator LookForEnemy()
    {
        var wait = new WaitForSeconds(_damageRate);

        while (DurationCoroutine != null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, RadiusSprite.bounds.max.x);

            foreach (Collider2D collider in hits)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    SuckHealth(enemy);
                }
            }

            yield return wait;
        }

        StopCoroutine(AbilityCoroutine);

        AbilityCoroutine = null;
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
