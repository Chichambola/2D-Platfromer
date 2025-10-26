using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class HealthIndidcatorsBase : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected Slider Slider;

    protected void Start()
    {
        Slider.value = Health.Value;
        Slider.maxValue = Health.MaxValue;
        ShowValue(Health.Value, Health.MaxValue);
    }

    protected void OnEnable()
    {
        Health.ValueChanged += ShowValue;
    }

    protected void OnDisable()
    {
        Health.ValueChanged -= ShowValue;
    }

    protected abstract void ShowValue(float health, float maxHealth);
}
