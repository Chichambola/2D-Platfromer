using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    private bool _isJump;
    private bool _isAbilityButtonPressed;
    
    public float Direction { get; private set; }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    public bool GetIsAbilityButtonPressed() => GetBoolAsTrigger(ref _isAbilityButtonPressed);

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        
        if(Input.GetKeyDown(KeyCode.W))
        { 
            _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isAbilityButtonPressed = true;
        }
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
