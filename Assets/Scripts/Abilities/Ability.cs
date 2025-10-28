using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{   
    [SerializeField] protected float Duration;
    [SerializeField] protected float Cooldown;
    [SerializeField] protected SpriteRenderer Sprite;
    
    protected float DurationDelay = 1f;
    protected Coroutine DurationCoroutine;
    protected Coroutine CooldownCoroutine;
    protected Coroutine AbilityCoroutine;
    
    public bool IsActive => DurationCoroutine != null || CooldownCoroutine != null;

    protected void Awake()
    {
        if(DurationCoroutine != null)
            StopCoroutine(DurationCoroutine);
        
        if(CooldownCoroutine != null)
            StopCoroutine(CooldownCoroutine);
        
        if (AbilityCoroutine != null)
            StopCoroutine(AbilityCoroutine);
    }

    public abstract void UseAbility();
    
    protected abstract IEnumerator DurationRoutine(float duration);
}
