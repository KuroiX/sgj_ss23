using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] private Slider cooldownSlider;
    private float _sliderProgress;
    
    // Start is called before the first frame update
    void Awake()
    {
        _sliderProgress = 1;
    }

    private void Start()
    {
        cooldownSlider.value = _sliderProgress;
    }

    private void Update()
    {
        //test
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCooldown(3);
        }
    }

    public void StartCooldown(float duration)
    {
        if (duration <= 0) return;
        StartCoroutine(Cooldown(duration));
    }

    IEnumerator Cooldown(float duration)
    {
        while (_sliderProgress > 0)
        {
            cooldownSlider.value = _sliderProgress;
            _sliderProgress -= Time.deltaTime / duration;
            yield return null;
        }
        _sliderProgress = 1;
    }
}
