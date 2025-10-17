using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerDetector))]
public class Chaser : MonoBehaviour
{
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private float _chaseSpeedMultiplier = 2.5f;

    private bool _isChasing = false;
    private Coroutine _coroutine;
    
    private void Awake()
    {
        _playerDetector = GetComponent<PlayerDetector>();
    }

    private void OnEnable()
    {
        _playerDetector.PlayerDetected += InitiateChase;
        _playerDetector.PlayerLost += StopChase;
    }

    private void OnDisable()
    {
        _playerDetector.PlayerDetected -= InitiateChase;
        _playerDetector.PlayerLost -= StopChase;
    }

    private void InitiateChase(Player player)
    {
        _isChasing = true;
        
        _coroutine = StartCoroutine(Chase(player));
    }

    private IEnumerator Chase(Player player)
    {
        while (_isChasing)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, new Vector2(player.transform.position.x, gameObject.transform.position.y), _chaseSpeedMultiplier * Time.deltaTime);

            yield return null;
        }
    }

    private void StopChase()
    {
        _isChasing = false;
        
        StopCoroutine(_coroutine);
    }
}
