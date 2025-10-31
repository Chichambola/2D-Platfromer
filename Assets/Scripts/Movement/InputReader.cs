using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const string Horizontal = nameof(Horizontal);
    private bool _isJump;
    private bool _isVampirismButtonPressed;
    private bool _isGunButtonPressed;
    
    public float Direction { get; private set; }

    public bool GetIsJump() => GetBoolAsTrigger(ref _isJump);
    public bool GetIsVampirismButtonPressed() => GetBoolAsTrigger(ref _isVampirismButtonPressed);
    public bool GetIsGunButtonPressed() => GetBoolAsTrigger(ref _isGunButtonPressed);

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);
        
        if(Input.GetKeyDown(KeyCode.W))
        { 
            _isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _isVampirismButtonPressed = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _isGunButtonPressed = true;
        }
    }

    private bool GetBoolAsTrigger(ref bool value)
    {
        bool localValue = value;
        value = false;
        return localValue;
    }
}
