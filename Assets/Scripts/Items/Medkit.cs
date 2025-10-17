using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : Item
{
    [SerializeField] private float _healAmount = 10;

    
    public float Heal()
    {
        return _healAmount;
    }
}
