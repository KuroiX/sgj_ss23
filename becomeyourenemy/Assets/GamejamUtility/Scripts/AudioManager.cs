using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public float MasterVolume
    {
        get => _masterVolume;
        set
        {
            audioMixer.SetFloat("MasterVolume", value);
            _masterVolume = value;
        }
    }
    public float MusicVolume
    {
        get => _musicVolume;
        set
        {
            audioMixer.SetFloat("MusicVolume", value);
            _musicVolume = value;
        }
    }
    public float SoundEffectVolume
    {
        get => _soundEffectVolume;
        set
        {
            audioMixer.SetFloat("SoundEffectVolume", value);
            _soundEffectVolume = value;
        }
    }
    public float EnvironmentVolume
    {
        get => _environmentVolume;
        set
        {
            audioMixer.SetFloat("EnvironmentVolume", value);
            _environmentVolume = value;
        }
    }

    [Header("Assign in Editor")] 
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] soundEffectClips;
    [SerializeField] private AudioClip[] environmentClips;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerSnapshot unpausedSnapshot;
    [SerializeField] private AudioMixerSnapshot pausedSnapshot;
    
    private float _masterVolume;
    private float _musicVolume;
    private float _soundEffectVolume;
    private float _environmentVolume;
    
    private AudioSource[] _audioSources;

    private void Awake()
    {
        InitSingleton();
        _audioSources = GetComponents<AudioSource>();
        InitVolumes();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        PauseManager.PauseTriggered += OnPauseTriggered;
        OnPauseTriggered(PauseManager.IsPaused);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        PauseManager.PauseTriggered -= OnPauseTriggered;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded");
        var container = FindObjectOfType<AudioContainer>();

        if (!container) return;

        if (container.BackgroundMusicClip == _audioSources[0].clip) return;
        
        FadeToClip(container.BackgroundMusicClip, 0.5f);
    }

    private void InitVolumes()
    {
        _masterVolume = FetchFloat("MasterVolume");
        _musicVolume = FetchFloat("MusicVolume");
        _soundEffectVolume = FetchFloat("SoundEffectVolume");
        _environmentVolume = FetchFloat("EnvironmentVolume");
    }

    private void InitSingleton()
    {
        if (!Instance)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private float FetchFloat(string floatName)
    {
        bool wasSuccessful = audioMixer.GetFloat(floatName, out var result);

        if (!wasSuccessful)
        {
            Debug.LogError($"Float with name {floatName} does not exist for audio mixer {audioMixer}.");
        }

        return result;
    }
    
    private void OnPauseTriggered(bool isPaused)
    {
        if (isPaused)
        {
            pausedSnapshot.TransitionTo(0f);
        }
        else
        {
            unpausedSnapshot.TransitionTo(0f);
        }
    }

    private void PlayClip(AudioClip clip)
    {
        _audioSources[0].clip = clip;
        _audioSources[0].Play();
    }
    
    private IEnumerator FadeToClipRoutine(AudioClip clip, float duration)
    {
        //Debug.Log("FadeTo");
        float currentTime = 0;
        float currentVolume = MasterVolume;

        while (currentTime < duration)
        {
            MasterVolume = currentVolume-(currentTime/duration * (80f-currentVolume));
            currentTime += Time.deltaTime;
            yield return null;
        }
        
        MasterVolume = (currentVolume);
        PlayClip(clip);
    }
    
    public void FadeToClipAt(int index, float duration = 0)
    {
        FadeToClip(musicClips[index], duration);
    }

    public void FadeToClip(AudioClip clip, float duration = 0)
    {
        StartCoroutine(FadeToClipRoutine(clip, duration));
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _audioSources[1].PlayOneShot(clip);
    }

    public void PlaySoundEffectAt(int index)
    {
        PlaySoundEffect(soundEffectClips[index]);
    }

    public void PlayEnvironment(AudioClip clip)
    {
        _audioSources[2].PlayOneShot(clip);
    }

    public void PlayEnvironmentAt(int index)
    {
        PlayEnvironment(environmentClips[index]);
    }
}