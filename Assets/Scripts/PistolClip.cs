using UnityEngine;

public class PistolClip : MonoBehaviour
{
    [SerializeField] private GameObject clipPrefab;
    private Rigidbody rb;

    private bool summonNew = false;
    public bool CanReload { get; private set; } = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SummonNew()
    {
        if(!summonNew)
        {
            summonNew = true;
            rb.useGravity = true;
            GameObject newClip = Instantiate(clipPrefab, transform.position, transform.rotation);
            newClip.transform.SetParent(transform.parent);
            transform.SetParent(null);
        }
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