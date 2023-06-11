using System.Collections;
using System.Collections.Generic;
using Controller.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    
    [Header("Health Attributes")]
    private float _currentSliderValue;
    private float maxHealth;
    private float _currentHealth;

    [SerializeField] private DefaultActions actions;
    // Start is called before the first frame update
    void Start()
    {
        _currentSliderValue = 1;
        maxHealth = actions.stats.health;
        Debug.Log(actions.stats.health);
        healthText.text = _currentHealth + " / " + maxHealth;
        _currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthSlider.value = _currentHealth / maxHealth;
        healthText.text = _currentHealth + " / " + maxHealth;
    }
    
    public void TakeDamage(float damage, int multiplier)
    {
        if (_currentHealth - damage <= 0)
        {
            _currentHealth = 0;
        } else 
            _currentHealth -= damage * multiplier;
    }
}
