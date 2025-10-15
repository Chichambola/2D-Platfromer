using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector2 _rotateRight = new Vector2(0,0);
    private Vector2 _rotateLeft = new Vector2(0,-180);
    
    public void Flip(float direction)
    {
        if (direction > 0)
        {
            gameObject.transform.eulerAngles = _rotateRight;
        }
        else if (direction < 0)
        {
            gameObject.transform.eulerAngles = _rotateLeft;
        }
    }
}
