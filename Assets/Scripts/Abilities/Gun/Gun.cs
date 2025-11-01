using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : Ability
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _lookForRadius = 15f;

    public override event Action<float, float> ValuesChanged;
    public override event Action<float> DurationStarted;
    public override event Action DurationCompleted;

    protected override void OnEnable()
    {
        Finder.EnemyFound += ShootBullet;
    }

    protected override void OnDisable()
    {
        Finder.EnemyFound += ShootBullet;
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

        DurationStarted?.Invoke(_lookForRadius);

        Sprite.enabled = true;

        yield return DurationRoutine(Duration);

        DurationCompleted?.Invoke();

        Sprite.enabled = false;

        yield return DurationRoutine(Cooldown);

        StopCoroutine(Coroutine);

        ChangeActiveState();
    }

    private void ShootBullet(Enemy enemy)
    {
        Bullet bullet = Instantiate(_bullet);

        bullet.transform.position = gameObject.transform.position;

        bullet.FollowTarget(enemy);
    }
}
