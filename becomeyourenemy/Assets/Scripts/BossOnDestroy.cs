using UnityEngine;

public class BossOnDestroy : MonoBehaviour
{

    [SerializeField] private SceneLoader sceneLoader;

    private void OnDestroy()
    {
        GameObject.Find("UI").GetComponent<UIManager>().ShowVictoryScreen();
    }

}
