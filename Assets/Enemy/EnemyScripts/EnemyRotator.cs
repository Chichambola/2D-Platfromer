using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotator : MonoBehaviour
{
    private Vector2 rotateRight = new Vector2(0,0);
    private Vector2 rotateLeft = new Vector2(0,-180);

    public Vector2 Flip(bool isMovingRight)
    {
        if (isMovingRight)
        {
            return rotateLeft;
        }
        else
        {
            return rotateRight;
        }
    }
}
