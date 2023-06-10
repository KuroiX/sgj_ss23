using UnityEngine;

namespace EnemyAI
{
    public class BossOnDestroy : MonoBehaviour
    {
    
        [SerializeField] private SceneLoader sceneLoader;
    
        private void OnDestroy()
        {
            sceneLoader.LoadNextScene();
        }
    
    }
}
