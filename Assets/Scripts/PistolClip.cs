using UnityEngine;

public class PistolClip : MonoBehaviour
{
    public ClipSpawner clipSpawner;
    private Rigidbody rb;

    private bool firstTimeGrab = false;
    public bool CanReload { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(!firstTimeGrab)
        {
            transform.position = clipSpawner.transform.position;
            transform.rotation = clipSpawner.transform.rotation;
        }
    }
    public void Grabbed()
    {
        if(!firstTimeGrab)
        {
            firstTimeGrab = true;
            clipSpawner.SpawnClipDelayed(1);
        }
    }

    public void Released()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
        transform.SetParent(null);
    }

    public void ReloadOn()
    {
        CanReload = true;
    }

    public void ReloadOff()
    {
        CanReload = false;
    }
}