using Unity.VisualScripting;
using UnityEngine;

public class ClipSpawner : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject clipPrefab;
    
    private void Start()
    {
        mainCamera = FindFirstObjectByType<Camera>();
        UpdatePosition();
        SpawnClip();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void SpawnClip()
    {
        Instantiate(clipPrefab, transform.position, transform.rotation, transform);
    }

    public void SpawnClipDelayed(float seconds)
    {
        Invoke("SpawnClip", seconds);
    }

    private void UpdatePosition()
    {
        Vector3 pos = transform.position;
        pos.y = mainCamera.transform.position.y / 2;
        transform.position = pos;
    }
}