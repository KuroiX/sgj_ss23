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
        Debug.Log("Start() in GameController");
        MusicAndSound.Instance.ResetCharacter();
        MusicAndSound.Instance.StopLevelMusic();
        MusicAndSound.Instance.PlayLevelMusic();
    }
}