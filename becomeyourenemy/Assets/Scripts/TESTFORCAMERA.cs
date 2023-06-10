using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TESTFORCAMERA : MonoBehaviour
{
    public CinemachineVirtualCamera c;
    private void Start()
    {
        SwitchCameraScript.currentCamera = c;
    }
}
