using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public event Action<Coin> CoinCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Coin coin))
        {
            CoinCollected?.Invoke(coin);
        }
    }
}
