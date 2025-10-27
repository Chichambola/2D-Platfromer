using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vampirism : Ability
{
    [SerializeField] private float _damage = 1f;

    public override void UseAbility()
    {
        if (DurationCoroutine != null)
            StopCoroutine(DurationCoroutine);
        
        gameObject.SetActive(true);
        
        DurationCoroutine = StartCoroutine(DurationRoutine());
    }
    
    protected override IEnumerator DurationRoutine()
    {
        var wait = new WaitForSeconds(DurationDelay);

        float currentDuration = 0;
        
        while (currentDuration != Duration)
        {
            currentDuration += DurationDelay;

            Debug.Log($"Duration: {currentDuration}");

            yield return wait;
        }

        StopCoroutine(DurationCoroutine);

        yield return CooldownCoroutine = StartCoroutine(CooldownRoutine(currentDuration));
    }

    protected override IEnumerator CooldownRoutine(float currentDuration)
    {
        gameObject.SetActive(false);

        var wait = new WaitForSeconds(DurationDelay);

        while (currentDuration != 0)
        {
            currentDuration -= DurationDelay;

            Debug.Log($"Cooldown: {currentDuration}");

            yield return wait;
        }

        StopCoroutine(CooldownCoroutine);
    }

    private IEnumerator SuckHealth(Enemy defender)
    {
        while(enabled)
        {
            defender.TakeDamage(_damage);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Enemy defender) && DurationCoroutine != null)
        {
            AbilityCoroutine = StartCoroutine(SuckHealth(defender));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy _))
        {
            StopCoroutine(AbilityCoroutine);
        }
    }
}
