using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private float _spawnDelay = 1.5f;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxCapacity = 5;
    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private Collector _coinCollector;

    private ObjectPool<Coin> _pool;

    private Coroutine _coroutine;
    
    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => Instantiate(_coinPrefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxCapacity);
    }

    private void Start()
    {
        _coroutine = StartCoroutine(Spawn());
    }

    private void ActionOnRelease(Coin coin)
    {
        coin.gameObject.SetActive(false);

        _coinCollector.CoinCollected -= Release;
    }

    private void Release(Coin coin)
    {
        if (coin.gameObject.activeSelf) 
        {
            _pool.Release(coin);
        }
    }
    
    private void GetCoin()
    {
        _pool.Get();
    }
    
    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnDelay);
        
        while (_poolCapacity != _poolMaxCapacity)
        {
            GetCoin();
            
            yield return wait;
        }
    }
    
    private void ActionOnGet(Coin coin)
    {
        int firstIndex = 0;
        
        int randomIndex = Random.Range(firstIndex, _spawnPoints.Count);
        
        coin.gameObject.transform.position = _spawnPoints[randomIndex].transform.position;
        
        coin.gameObject.SetActive(true);

        _coinCollector.CoinCollected += Release;
    }
}
