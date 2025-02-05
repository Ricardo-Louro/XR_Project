using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HandPhysics : MonoBehaviour
{
    [SerializeField] bool isLeftHand;
    [SerializeField] float teleportingDistance = .5f;
    Transform handController;
    Rigidbody rb;
    private Collider[] handColliders;

    //In inspector: Mark if Left hand, adjust teleleporting distance (hand come sback if too far)
    //Assign in the Left & Right Controller Near-Far Interactor respectively the 'Interaction Event' Select Exited DisableHandColliders()
    //Assign in the Left & Right Controller Near-Far Interactor respectively the 'Interaction Event' Select EnableHandCollidersDelay()

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        handColliders = GetComponentsInChildren<Collider>();
        transform.SetParent(null);
        if (isLeftHand)
        {
            handController = GameObject.Find("Left Controller").transform;
        }
        else
        {
            handController = GameObject.Find("Right Controller").transform;
        }
    }

    public void EnableHandColliders()
    {
        foreach (Collider collider in handColliders)
        {
            collider.enabled = true;
        }
    }

    public void EnableHandCollidersDelay(float delay)
    {
        Invoke("EnableHandColliders", delay); // ~.5f
    }

    public void DisableHandColliders()
    {
        foreach (Collider collider in handColliders)
        {
            collider.enabled = false;
        }
    }
}
