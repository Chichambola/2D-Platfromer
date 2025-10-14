using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    private bool _isJump;

    public float Direction { get; private set; }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        
        Debug.Log(Direction);
        
        if(Input.GetKeyDown(KeyCode.W))
        { 
            _isJump = true;
        }
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
