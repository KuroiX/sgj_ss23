using UnityEngine;
using UnityEngine.UI;

public class AudioSliderManager : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider effectSlider;
    [SerializeField] private Slider environmentSlider;
    
    private AudioManager _audioManager;
    
    private void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();
        SetSlider();
    }
    
    private void SetSlider()
    {
        masterSlider.value = _audioManager.MasterVolume;
        effectSlider.value = _audioManager.SoundEffectVolume;
        musicSlider.value = _audioManager.MusicVolume;
        environmentSlider.value = _audioManager.EnvironmentVolume;
    }
    
    public void OnMasterChanged(float value)
    {
        _audioManager.MasterVolume = value;
    }
    
    public void OnEffectChanged(float value)
    {
        _audioManager.SoundEffectVolume = value;
    }
    
    public void OnMusicChanged(float value)
    {
        _audioManager.MusicVolume = value;        
    }
    
    public void OnEnvironmentChanged(float value)
    {
        _audioManager.EnvironmentVolume = value;
    }
}