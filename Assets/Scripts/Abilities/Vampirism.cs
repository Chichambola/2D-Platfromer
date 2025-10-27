using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vampirism : Ability
{
    public override void Use()
    {
        if (Coroutine != null)
            StopCoroutine(Coroutine);
        
        gameObject.SetActive(true);
        
        Coroutine = StartCoroutine(DurationRoutine());
    }
    
    protected override IEnumerator DurationRoutine()
    {
        var wait = new WaitForSeconds(DurationDelay);

        float currentDuration = 0;
        
        while (currentDuration != Duration)
        {
            currentDuration += DurationDelay;
            
            Debug.Log(currentDuration);
            
            yield return wait;
        }
        
        yield return Coroutine = StartCoroutine(CooldownRoutine());
    }

    protected override IEnumerator CooldownRoutine()
    {
        gameObject.SetActive(false);
        
        yield return null;
    }
}
