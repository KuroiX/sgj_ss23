using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class LoadNextSceneOnKey : MonoBehaviour
{
    [SerializeField] private Key[] keys;

    private KeyControl[] _keyControls;

    private SceneLoader _sceneLoader;
    
    private void Awake()
    {
        _sceneLoader = FindObjectOfType<SceneLoader>().GetComponent<SceneLoader>();
        _keyControls = Keyboard.current.allKeys.Where(key => keys.Contains(key.keyCode)).ToArray();
    }

    private void Update()
    {
        if (!_keyControls.Any(key => key.wasReleasedThisFrame)) return;

        _sceneLoader.LoadNextScene();
    }
}