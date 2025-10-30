using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{   
    [SerializeField] protected float Duration;
    [SerializeField] protected float Cooldown;
    [SerializeField] protected SpriteRenderer RadiusSprite;
    
    protected float DurationDelay = 1f;
    protected float CurrentDuration;
    protected Coroutine DurationCoroutine;
    protected Coroutine AbilityCoroutine;

    public abstract event Action<float, float> ValuesChanged;
    
    public bool IsActive { get; private set; }

    protected void Start()
    {
        IsActive = false;

        if(DurationCoroutine != null)
            StopCoroutine(DurationCoroutine);
        
        if (AbilityCoroutine != null)
            StopCoroutine(AbilityCoroutine);
    }

    public abstract void UseAbility();
    
    protected void ToggleActive()
    {
        if (IsActive)
            IsActive = false;
        else 
            IsActive = true;
    }

    protected abstract IEnumerator DurationRoutine(float duration);
}
