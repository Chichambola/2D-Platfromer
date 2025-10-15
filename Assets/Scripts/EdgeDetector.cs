using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EdgeDetector : MonoBehaviour
{
    private float _distanceToCheck = 3f;

    public event Action OffEdgeDetected;

    private void FixedUpdate()
    {
        RaycastHit2D hitDownwards = Physics2D.Raycast(gameObject.transform.position, Vector2.down, _distanceToCheck);

        if (hitDownwards.collider == null)
        {
            OffEdgeDetected?.Invoke();
        }
    }
}
