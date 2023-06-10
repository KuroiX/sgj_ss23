using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Characters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private UIManager _uiManager;

    private DefaultActions currentActions;
    
    [Header("Health Attributes")]
    private float _currentSliderValue;
    private float maxHealth;
    private float _currentHealth;
    private bool _dead;

    public int currentKillCount;

    private bool _updateHealth;
    
    // Start is called before the first frame update
    void Awake()
    {
        _currentSliderValue = 1;
        _dead = false;
        currentKillCount = 0;
        _updateHealth = false;
    }

    private void Start()
    {
        currentActions = GetComponentInChildren<DefaultActions>();
        maxHealth = currentActions.stats.health;
        _currentHealth = maxHealth;
        healthText.text = _currentHealth + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(_updateHealth);
        if (_updateHealth)
        {
            maxHealth = GetComponentInChildren<DefaultActions>().stats.health;
            _currentHealth = maxHealth;
            _updateHealth = false;
        }
        healthSlider.value = _currentHealth / maxHealth;
        healthText.text = _currentHealth + " / " + maxHealth;
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1f, 1);
        }*/

        if (_dead)
        {
            Debug.Log("dead");
        }
    }

    public void TakeDamage(float damage, int multiplier)
    {
        if (_currentHealth - damage <= 0)
        {
            _currentHealth = 0;
            _dead = true;
        } else 
            _currentHealth -= damage * multiplier;
    }

    public void Heal(float amount)
    {
        if (_currentHealth + amount >= maxHealth)
        {
            _currentHealth = maxHealth;
        }
        else
            _currentHealth += amount;
    }

    public void Respawn()
    {
        _currentHealth = maxHealth;
    }

    public void UpdateHealth()
    {
        _updateHealth = true;
    }

    public void UpdateKillCount()
    {
        currentKillCount++;
        _uiManager.killCount = currentKillCount;
    }
}
