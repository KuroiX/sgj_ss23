using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI enemiesAbsorbedText;
    [SerializeField] private TextMeshProUGUI yourTimeText;
    [SerializeField] private TextMeshProUGUI allTimeBestText;

    [SerializeField] private UIManager uiManager;

    private PlayerHealth _playerStats;
    // Start is called before the first frame update
    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemiesAbsorbedText.text = "Enemies absorbed: " + uiManager.killCount;
        yourTimeText.text = "Your time: " + uiManager.speedrunTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerHealth>();
        enemiesAbsorbedText.text = "Enemies absorbed: " + uiManager.killCount;
        yourTimeText.text = "Your time: " + uiManager.speedrunTime;

    }
}
