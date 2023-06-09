using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    public AudioClip BackgroundMusicClip => backgroundMusicClip;
    
    [SerializeField] private AudioClip backgroundMusicClip;
}