using Controller;
using UnityEngine;

public class BossOnDestroy : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    private void OnDestroy()
    {
        //GameObject.Find("Player").GetComponent<PlayerInput>()._characterInput.Disable();
        //Time.timeScale = 0;
        GameObject.Find("UI").GetComponent<UIManager>().ShowVictoryScreen();
    }
}
