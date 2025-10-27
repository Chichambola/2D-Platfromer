using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{   
    [SerializeField] protected float Duration;
    [SerializeField] protected float Cooldown;

    public bool IsOnCooldown => CooldownCoroutine != null;
    protected float DurationDelay = 1f;

    protected Coroutine DurationCoroutine;
    protected Coroutine CooldownCoroutine;
    protected Coroutine AbilityCoroutine;

    public abstract void UseAbility();
    
    protected abstract IEnumerator DurationRoutine();

    protected abstract IEnumerator DurationRoutine(float currenDuration);
}
