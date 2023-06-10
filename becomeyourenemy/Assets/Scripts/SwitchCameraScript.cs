using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCameraScript : MonoBehaviour
{
    public static CinemachineVirtualCamera currentCamera;
    [SerializeField] private CinemachineVirtualCamera roomCamera;


    private void OnTriggerEnter2D(Collider2D other)
    {
        currentCamera.Priority -= 1;
        roomCamera.Priority += 1;
        currentCamera = roomCamera;
    }
}
