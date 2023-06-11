using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedrunTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [HideInInspector] public float elapsedTime;
    private bool timerActive;
    private Coroutine _cr;
    private TimeSpan timeSpan;

    [SerializeField] private UIManager _uiManager;


    // Start is called before the first frame update
    void Awake()
    {
        elapsedTime = 0;
    }

    private void Start()
    {
        StartTimer();
    }
    
    public void StartTimer()
    {
        timerActive = true;
        elapsedTime = 0f;
        _cr = StartCoroutine(UpdateTimer());
    }

    public string StopTimer()
    {
        StopCoroutine(_cr);
        timeSpan = TimeSpan.FromSeconds(elapsedTime);
        timerActive = false;
        
        return timeSpan.ToString("mm':'ss'.'ff");
    }

    private IEnumerator UpdateTimer()
    {
        while (timerActive)
        {
            elapsedTime += Time.deltaTime;
            timeSpan = TimeSpan.FromSeconds(elapsedTime);
            _uiManager.speedrunTime = timeSpan.ToString("mm':'ss'.'ff");
            _uiManager.rawSpeedrunTime = elapsedTime;
            timerText.text = "Time: " + timeSpan.ToString("mm':'ss'.'ff");
            yield return null;
        }
    }
}
