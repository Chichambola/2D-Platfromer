using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyMover), typeof(EnemyRotator))]
public class Bear : MonoBehaviour
{
    private EnemyMover _enemyMover;
    private EnemyRotator _enemyRotator;
    
    private void Awake()
    {
        _enemyMover = GetComponent<EnemyMover>();
        _enemyRotator = GetComponent<EnemyRotator>();
    }
}
