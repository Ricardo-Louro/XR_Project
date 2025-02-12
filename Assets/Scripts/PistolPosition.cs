using UnityEngine;

public class PistolPosition : MonoBehaviour
{ 
    private Camera mainCamera;
    private Transform playerTransform;


    private void Start()
    {
        mainCamera = FindFirstObjectByType<Camera>();
        playerTransform = FindFirstObjectByType<PlayerWallBlock>().transform;
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        Vector3 pos = transform.position;
        pos.y = mainCamera.transform.position.y * .5f;
        transform.position = pos;
    }
}
