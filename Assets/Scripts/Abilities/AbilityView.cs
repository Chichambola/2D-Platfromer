using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AbilityView : MonoBehaviour
{
    [SerializeField] private Ability _ability;

    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();    
    }

    private void OnEnable()
    {
        _ability.ValuesChanged += ShowValue;
    }

    private void OnDisable()
    {
        _ability.ValuesChanged -= ShowValue;
    }

    private void ShowValue(float currentValue, float maxValue)
    {
        _text.text = $"{maxValue} / {currentValue}";
    }
}
