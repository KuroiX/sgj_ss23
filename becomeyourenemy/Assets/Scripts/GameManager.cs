using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float[] fastestCompletionTime;
    [SerializeField] private int levelAmount;
    

    // Start is called before the first frame update
    void Start()
    {
        fastestCompletionTime = new float[levelAmount];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
