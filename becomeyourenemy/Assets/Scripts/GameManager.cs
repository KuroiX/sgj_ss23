using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float[] allTimeBests;
    private static GameManager instance;
    private string[] keys;
    //[SerializeField] private int levelAmount;

    private GameManager()
    {
        if (instance != null)
            return;
        instance = this;
    }
    
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }
    
    private void Awake()
    {
        LoadPrefs();
    }

    void Start()
    {
        allTimeBests = new float[SceneManager.sceneCount - 2]; //TODO anpassen wieviel scenes ausser levels
        keys = new string[SceneManager.sceneCount - 2]; //TODO anpassen wieviel scenes ausser levels
        for (int i = 0; i < allTimeBests.Length; i++)
        {
            keys[i] = "AllTimeBest" + i;
        }
    }
    
    private void OnApplicationQuit()
    {
        SavePrefs();
    }

    private void SavePrefs()
    {
        foreach (var key in keys)
        {
            //PlayerPrefs.SetString(key, );
        }
        //}
        PlayerPrefs.Save();
    }

    private void LoadPrefs()
    {
        PlayerPrefs.Save();
    }
}
