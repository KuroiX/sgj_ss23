using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int killCount;
    [HideInInspector] public string speedrunTime;
    [HideInInspector] public float rawSpeedrunTime;
    [SerializeField] private GameObject victoryScreen;
    
    // Start is called before the first frame update
    void Awake()
    {
        killCount = 0;
    }

    public void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
    }
}
