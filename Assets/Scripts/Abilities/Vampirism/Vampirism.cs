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

    public override event Action<float, float> ValuesChanged;
    public override event Action DurationCompleted;
    public override event Action<float> DurationStarted;
    public event Action<float> HealthStolen;

    protected override void OnEnable()
    {
        Finder.EnemyFound += SuckHealth;
    }

    protected override void OnDisable()
    {
        Finder.EnemyFound += SuckHealth;
    }

    public override void UseAbility()
    {
        Coroutine = StartCoroutine(PlayCoroutines());
    }

    protected override IEnumerator DurationRoutine(float duration)
    {
        var wait = new WaitForSeconds(DurationDelay);

        while (CurrentDuration != duration)
        {
            CurrentDuration += DurationDelay;

            ValuesChanged?.Invoke(CurrentDuration, duration);

            yield return wait;
        }

        CurrentDuration = 0;
    }

    private IEnumerator PlayCoroutines()
    {
        ChangeActiveState();

        DurationStarted?.Invoke(Sprite.bounds.extents.x);

        Sprite.enabled = true;

        yield return DurationRoutine(Duration);

        DurationCompleted?.Invoke();

        Sprite.enabled = false;

        yield return DurationRoutine(Cooldown);

        StopCoroutine(Coroutine);

        ChangeActiveState();
    }

    private void SuckHealth(Enemy defender)
    {
        defender.TakeDamage(_damage);

        HealthStolen?.Invoke(_damage);
    }
}
