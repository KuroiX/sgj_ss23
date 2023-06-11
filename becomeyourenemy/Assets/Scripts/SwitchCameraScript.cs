using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCameraScript : MonoBehaviour
{
    public static CinemachineVirtualCamera CurrentCamera;
    [SerializeField] private CinemachineVirtualCamera roomCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        CurrentCamera.Priority -= 1;
        roomCamera.Priority += 1;
        CurrentCamera = roomCamera;
    }
}
