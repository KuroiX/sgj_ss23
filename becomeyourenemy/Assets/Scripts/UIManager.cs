using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public int killCount;
    [HideInInspector] public string speedrunTime;
    [HideInInspector] public float rawSpeedrunTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        killCount = 0;
    }
}
