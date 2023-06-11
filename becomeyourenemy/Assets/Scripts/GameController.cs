using System;
using Cinemachine;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera startCamera;
    
    private void Awake()
    {
        SwitchCameraScript.CurrentCamera = startCamera;
    }

    private void Start()
    {
        MusicAndSound.Instance.PlayLevelMusic();
    }
}