using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    private Vector3 _rotateRight = new Vector2(0, 0);
    private Vector3 _rotateLeft = new Vector2(0, 180);

    public void Flip()
    {
        if (gameObject.transform.localEulerAngles == _rotateRight)
        {
            gameObject.transform.localEulerAngles = _rotateLeft;
        }
        else if (gameObject.transform.localEulerAngles == _rotateLeft)
        {
            gameObject.transform.localEulerAngles = _rotateRight;
        }
    }

    public void Flip(float direction)
    {
        if (direction > 0)
        {
            gameObject.transform.localEulerAngles = _rotateRight;
        }
        else if (direction < 0)
        {
            gameObject.transform.localEulerAngles = _rotateLeft;
        }
    }
}
