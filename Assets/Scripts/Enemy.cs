using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody[] ragdollRigidbodies;

    private void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        DisableRagdoll();
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        SetLayer(gameObject, 3);

    }

    public void EnableRagdoll()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private static void SetLayer(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}