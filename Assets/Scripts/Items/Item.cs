using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> PickedUp;

    public void PickUp()
    {
        PickedUp?.Invoke(this);
    }
}
