using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Item _prefab;

    [SerializeField] private float _spawnDelay = 1.5f;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxCapacity = 5;

    [SerializeField] private List<SpawnPoint> _spawnPoints;
    [SerializeField] private Collector _collector;

    private Coroutine _coroutine;

    private ObjectPool<Item> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Item>(
            createFunc: () => Instantiate(_prefab),
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

    private void ActionOnRelease(Item item)
    {
        item.gameObject.SetActive(false);

        _collector.ItemCollected -= Release;
    }

    private void Release(Item item)
    {
        if (item.gameObject.activeSelf) 
        {
            _pool.Release(item);
        }
    }
    
    private void GetItem()
    {
        _pool.Get();
    }
    
    private IEnumerator Spawn()
    {
        var wait = new WaitForSeconds(_spawnDelay);
        
        while (_poolCapacity != _poolMaxCapacity)
        {
            GetItem();
            
            yield return wait;
        }
    }
    
    private void ActionOnGet(Item item)
    {
        int firstIndex = 0;
        
        int randomIndex = Random.Range(firstIndex, _spawnPoints.Count);

        item.gameObject.transform.position = _spawnPoints[randomIndex].transform.position;

        item.gameObject.SetActive(true);

        _collector.ItemCollected += Release;
    }
}
