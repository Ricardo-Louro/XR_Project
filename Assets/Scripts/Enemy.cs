using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool active;

    private Rigidbody[] ragdollRigidbodies;

    private Transform playerTransform;
    private Transform cameraTransform;

    [SerializeField] private float maxAttackRange;
    private float lastTimeAttacked = 0;
    private float maxAttackCooldown = 5;
    private float minAttackCooldown = 1;

    [SerializeField] private Transform firingTransform;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private AudioClip[] gunshotSound;
    [SerializeField] private AudioClip[] deathSound;
    private AudioSource audioSource;

    private void Awake()
    {
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        playerTransform = FindFirstObjectByType<PlayerWallBlock>().transform;
        cameraTransform = FindFirstObjectByType<Camera>().transform;
        audioSource = GetComponent<AudioSource>();
        DisableRagdoll();
    }

    private void Update()
    {
        if(active)
        {
            if(Mathf.Abs((transform.position - playerTransform.position).magnitude) <= maxAttackRange)
            {
                if(Time.time - lastTimeAttacked >= Random.Range(minAttackCooldown,maxAttackCooldown))
                {
                    Attack();
                }
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, cameraTransform.position - transform.position, out hit))
            {
                if (hit.collider.GetComponentInParent<PlayerWallBlock>() != null)
                {
                    active = true;
                }
            }
        }

    }

    private void Attack()
    {     
        lastTimeAttacked = Time.time;
        muzzleFlash.Play();
        audioSource.pitch = Random.Range(.7f, 1);
        audioSource.PlayOneShot(gunshotSound[Random.Range(0, gunshotSound.Length)]);

        RaycastHit hit;
        if(Physics.Raycast(firingTransform.position, playerTransform.position - firingTransform.position, out hit))
        {
            if(hit.collider.GetComponentInParent<PlayerWallBlock>() != null)
            {
                //DAMAGE
            }
            else
            {
                if(Physics.Raycast(firingTransform.position, cameraTransform.position - firingTransform.position, out hit))
                {
                    if(hit.collider.GetComponentInParent<PlayerWallBlock>() != null)
                    {
                        //DAMAGE
                    }
                }
            }
        }
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        SetLayer(gameObject, 3);

    }

    private void EnableRagdoll()
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

    public void Die()
    {
        audioSource.pitch = Random.Range(.7f, 1);
        audioSource.PlayOneShot(deathSound[Random.Range(0, deathSound.Length)]);
        EnableRagdoll();
        Destroy(GetComponent<Animator>());
        Destroy(this);
    }
}