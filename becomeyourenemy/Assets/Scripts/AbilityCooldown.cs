using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCooldown : MonoBehaviour
{
    [SerializeField] private Slider cooldownSlider;
    private float _timer;
    [HideInInspector] public bool coolingDown;
    
    // Start is called before the first frame update
    void Awake()
    {
        _timer = 1;
        coolingDown = false;
    }

    private void Start()
    {
        cooldownSlider.value = _timer;
    }

    private void Update()
    {
        //test
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCooldown(3);
        }*/
    }

    public void StartCooldown(float duration)
    {
        if (duration <= 0) return;
        StartCoroutine(Cooldown(duration));
    }

    IEnumerator Cooldown(float duration)
    {
        while (_timer > 0)
        {
            coolingDown = true;
            cooldownSlider.value = _timer;
            _timer -= Time.deltaTime / duration;
            yield return null;
        }
        _timer = 1;
        coolingDown = false;
    }
}
