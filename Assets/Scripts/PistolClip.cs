using UnityEngine;

public class PistolClip : MonoBehaviour
{
    public bool CanReload { get; private set; } = false;

    public void ReloadOn()
    {
        CanReload = true;
    }

    public void ReloadOff()
    {
        CanReload = false;
    }
}
