using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHandler : MonoBehaviour
{
    [SerializeField] private DamageZone _damageZone;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _damageZone.PlayerInZone += DamageEnemy;
    }

    private void OnDisable()
    {
        _damageZone.PlayerInZone -= DamageEnemy;
    }

    private void DamageEnemy(Player attacker, Enemy defender)
    {
        _player.PushFromEnemy();

        attacker.DealDamage(defender);
    }
}
