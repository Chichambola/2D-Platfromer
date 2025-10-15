using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
    private int _groundCounter;
    public bool IsGround => _groundCounter > 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _groundCounter++;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Ground _))
            _groundCounter--;
    }
}
