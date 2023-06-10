using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private Slider healthSlider;
    private float _currentHealth;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = _currentHealth;
    }

    void TakeDamage(float damage, int multiplier)
    {
        _currentHealth -= damage * multiplier;
    }

    void Heal(float amount)
    {
        _currentHealth += amount;
    }
}
