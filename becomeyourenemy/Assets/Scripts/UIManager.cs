using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public int killCount;
    public string speedrunTime;
    public float rawSpeedrunTime;
    
    // Start is called before the first frame update
    void Awake()
    {
        killCount = 0;
    }
}
