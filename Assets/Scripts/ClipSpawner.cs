using UnityEngine;

public class ClipSpawner : MonoBehaviour
{
    private Camera mainCamera;
    private Transform playerTransform;
    [SerializeField] private GameObject clipPrefab;
    
    private void Start()
    {
        mainCamera = FindFirstObjectByType<Camera>();
        playerTransform = FindFirstObjectByType<PlayerWallBlock>().transform;
        UpdatePosition();
        SpawnClip();
    }

    private void Update()
    {
        UpdatePosition();
    }

    private void SpawnClip()
    {
        GameObject clip = Instantiate(clipPrefab, transform.position, transform.rotation, transform);
        clip.GetComponent<PistolClip>().clipSpawner = this;
    }

    public void SpawnClipDelayed(float seconds)
    {
        Invoke("SpawnClip", seconds);
    }

    private void UpdatePosition()
    {
        Vector3 pos = transform.position;
        pos.y = mainCamera.transform.position.y * .5f;
        transform.position = pos;
    }
}