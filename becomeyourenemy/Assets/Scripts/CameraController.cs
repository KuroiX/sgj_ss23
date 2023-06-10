using UnityEngine;

public class CameraController : MonoBehaviour
{

    [SerializeField]
    private Camera mainCamera;

    private void Update()
    {

        Vector3 position = transform.position;
        Transform cameraTransform = mainCamera.transform;
        cameraTransform.position = new Vector3(position.x, position.y, cameraTransform.position.z);

    }
    
}
