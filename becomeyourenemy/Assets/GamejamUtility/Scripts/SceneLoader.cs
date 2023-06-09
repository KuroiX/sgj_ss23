using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public event Action<bool> LoadingTriggered
    {
        add => _boolEvent.EventTriggered += value;
        remove => _boolEvent.EventTriggered -= value;
    }

    public bool IsLoading
    {
        get => _boolEvent.IsActive;
        private set => _boolEvent.IsActive = value;
    }

    [Header("Set in Editor")]
    [Range(0, 5)]
    [SerializeField] private float fadeTime = 0.1f;
    [SerializeField] private bool useLoadingBar = false;
    [Range(0, 5)]
    [Tooltip("This value can be used to artificially increase load times for things like tips on screen. " +
             "If it is set to 0, the original loading time will be used, potentially resulting in no loading bar.")]
    [SerializeField] private float minLoadTime = 0.2f;
    

    [Header("Assign in Editor")] 
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject loadingBar;

    private Slider _loadingBarSlider;
    private Image _fadeImage;

    private BoolEvent _boolEvent;

    private void Awake()
    {
        loadingBar.SetActive(false);
        _loadingBarSlider = loadingBar.GetComponent<Slider>();
        fadePanel.SetActive(true);
        canvas.SetActive(true);
        _fadeImage = fadePanel.GetComponent<Image>();
        
        StartCoroutine(FadeScreen(false));
    }

    private IEnumerator LoadSceneByNameRoutine(string sceneName)
    {
        yield return StartCoroutine(FadeScreen(true));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if (!useLoadingBar) yield break;
        
        yield return StartCoroutine(LoadingBarRoutine(operation));
    }

    private IEnumerator LoadSceneByIndexRoutine(int sceneIndex)
    {
        yield return StartCoroutine(FadeScreen(true));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        if (!useLoadingBar) yield break;
        
        yield return StartCoroutine(LoadingBarRoutine(operation));
    }

    private IEnumerator LoadingBarRoutine(AsyncOperation operation)
    {
        operation.allowSceneActivation = false;
        
        _loadingBarSlider.value = 0;
        loadingBar.SetActive(true);

        float timer = minLoadTime;
        
        while (operation.progress < 0.9f || timer > 0)
        {
            float progress = Mathf.Clamp(operation.progress / 0.9f, 0, 1 - timer / minLoadTime);

            _loadingBarSlider.value = progress;
                
            yield return null;
            
            timer -= Time.deltaTime;
        }

        operation.allowSceneActivation = true;
    }

    private IEnumerator FadeScreen(bool isFadingToBlack)
    {
        IsLoading = true;

        _fadeImage.enabled = true;
        _fadeImage.color = new Color(0, 0, 0, isFadingToBlack ? 0 : 1);
        
        float currentTime = fadeTime;
        
        while (currentTime > 0)
        {
            float progress = (currentTime / fadeTime);

            float newAlpha = isFadingToBlack ? 1 - progress : progress;
            
            _fadeImage.color = new Color(0, 0, 0, newAlpha);
            
            //Debug.Log($"Fade by {newAlpha}");
            
            yield return null;
            
            currentTime -= Time.deltaTime;
        }
        
        _fadeImage.color = new Color(0, 0, 0, isFadingToBlack ? 1 : 0);
        _fadeImage.enabled = isFadingToBlack;
        IsLoading = isFadingToBlack;
    }
    
    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneByIndexRoutine((SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings));
    }

    public void LoadSceneByName(string sceneName)
    {
        StartCoroutine(LoadSceneByNameRoutine(sceneName));
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        StartCoroutine(LoadSceneByIndexRoutine(sceneIndex));
    }
}
