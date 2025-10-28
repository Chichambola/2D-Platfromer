using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vampirism : Ability
{
    [SerializeField] private float _damage = 1f;
    [SerializeField] private float _damageRate = 0.5f;

    public override void UseAbility()
    {
        Sprite.enabled = true;
        
        DurationCoroutine = StartCoroutine(DurationRoutine(Duration));
    }
    
    protected override IEnumerator DurationRoutine(float duration)
    {
        var wait = new WaitForSeconds(DurationDelay);
        
        float currentDuration = 0;
        
        while (currentDuration != duration)
        {
            currentDuration += DurationDelay;

            Debug.Log(currentDuration);
            
            yield return wait;
        }
        
        ToggleCoroutines();
        
        yield return null;
    }

    private void ToggleCoroutines()
    {
        if (DurationCoroutine != null)
        {
            Sprite.enabled = false;
            
            CooldownCoroutine = StartCoroutine(DurationRoutine(Cooldown));

            StopCoroutine(DurationCoroutine);
            
            DurationCoroutine = null;
        }
        else
        {
            StopCoroutine(CooldownCoroutine);
            
            CooldownCoroutine = null;
        }
    }
    
    private IEnumerator SuckHealth(Enemy defender)
    {
        var wait = new WaitForSeconds(_damageRate);
        
        while(enabled)
        {
            defender.TakeDamage(_damage);

            yield return wait;
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
