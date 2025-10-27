using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{   
    [SerializeField] protected float Duration;
    [SerializeField] protected float Cooldown;

    public bool IsActive => Coroutine != null;
    protected float DurationDelay = 1f;
    protected Coroutine Coroutine;

    public abstract void Use();
    
    protected abstract IEnumerator DurationRoutine();
    
    protected abstract IEnumerator CooldownRoutine();
}
