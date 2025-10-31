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
    protected float CurrentDuration;
    protected Coroutine DurationCoroutine;
    protected Coroutine Coroutine;

    public abstract event Action<float, float> ValuesChanged;
    
    public bool IsActive { get; private set; }

    private void Start()
    {
        IsActive = false;

        if(DurationCoroutine != null)
            StopCoroutine(DurationCoroutine);
        
        if (Coroutine != null)
            StopCoroutine(Coroutine);
    }

    public abstract void UseAbility();
    
    protected void ChangeActiveState()
    {
        if (IsActive)
            IsActive = false;
        else 
            IsActive = true;
    }

    protected abstract IEnumerator DurationRoutine(float duration);
}
