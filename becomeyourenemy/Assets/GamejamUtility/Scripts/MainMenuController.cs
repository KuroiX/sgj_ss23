using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("Assign in Editor")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject todosPanel;
    
    private SceneLoader _sceneLoader;
    
    private void Awake()
    {
        todosPanel.SetActive(false);
        ActivatePanel(mainPanel);
        
        _sceneLoader = FindObjectOfType<SceneLoader>();
    }
    
    public void Play()
    {
        //Debug.Log("Play!");
        _sceneLoader.LoadNextScene();
    }
    
    public void SelectLevel(int levelIndex)
    {
        //Debug.Log($"Level Index: {levelIndex}");
        _sceneLoader.LoadSceneByIndex(levelIndex);
    }
    
    public void Exit()
    {
        //Debug.Log("Exit!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else        
        Application.Quit();
#endif
    }
    
    public void ActivateMain()
    {
        ActivatePanel(mainPanel);
    }
    
    public void ActivateLevelSelection()
    {
        ActivatePanel(levelSelectPanel);
    }
    
    public void ActivateTutorial()
    {
        ActivatePanel(tutorialPanel);
    }
    
    public void ActivateCredits()
    {
        ActivatePanel(creditsPanel);
    }
    
    public void ActivateSettings()
    {
        ActivatePanel(settingsPanel);
    }
    
    private void ActivatePanel(GameObject panel)
    {
        mainPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        creditsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
        settingsPanel.SetActive(false);
        
        panel.SetActive(true);
    }
}
