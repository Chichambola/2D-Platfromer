using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxCapacity = 5;

    private ObjectPool<Coin> _pool;

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

    private void ActionOnRelease(Coin coin)
    {

    }

    private void ActionOnGet(Coin coin)
    {

    }
}
