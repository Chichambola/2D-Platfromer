using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DamageZone : MonoBehaviour
{
    public event Action<Player, Enemy> PlayerInZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var defender = GetComponentInParent<Enemy>();

        if (collision.TryGetComponent(out Player attacker) && defender != null)
        {
            PlayerInZone?.Invoke(attacker, defender);
        }
    }
}
