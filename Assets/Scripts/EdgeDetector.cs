using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EdgeDetector : MonoBehaviour
{
    private float _distanceToCheckEdge = 2f;

    public event Action OffEdgeDetected;

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, Vector2.down, _distanceToCheckEdge);

        if (hit.collider == null)
        {
            OffEdgeDetected?.Invoke();
        }
    }
}
