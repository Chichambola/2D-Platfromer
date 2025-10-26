using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarSmooth : HealthIndidcatorsBase
{
    [SerializeField] private float _smoothSpeed;

    private Coroutine _coroutine;

    private void Awake()
    {
        Slider = GetComponent<Slider>();
    }

    protected override void ShowValue(float health, float maxHealth)
    {
        Slider.maxValue = maxHealth;
        
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeValueSmoothly(health));
    }

    private IEnumerator ChangeValueSmoothly(float health)
    {
        while (Slider.value != health) 
        {
            Slider.value = Mathf.MoveTowards(Slider.value, health, _smoothSpeed);

            yield return null;
        }
    }
}
