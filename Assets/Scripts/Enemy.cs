using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool active;

    private EndGameHandler handler;

    private Vector3 lookAtVector;

    private Rigidbody[] ragdollRigidbodies;

    private Transform playerTransform;
    private Transform cameraTransform;

    private PlayerHealth playerHealth;

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
        handler = FindFirstObjectByType<EndGameHandler>();

        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        playerTransform = FindFirstObjectByType<PlayerWallBlock>().transform;
        cameraTransform = FindFirstObjectByType<Camera>().transform;
        playerHealth = FindFirstObjectByType<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();
        DisableRagdoll();

        lookAtVector.y = transform.position.y;
    }

    private void Start()
    {
        handler.AddToList(this);
    }

    private void Update()
    {
        if(active)
        {
            lookAtVector.x = playerTransform.position.x;
            lookAtVector.z = playerTransform.position.z;
            transform.LookAt(lookAtVector);

            if (Mathf.Abs((firingTransform.position - playerTransform.position).magnitude) <= maxAttackRange)
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
            if(Physics.Raycast(firingTransform.position, cameraTransform.position - firingTransform.position, out hit))
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
                playerHealth.Hurt();
            }
            else
            {
                if(Physics.Raycast(firingTransform.position, cameraTransform.position - firingTransform.position, out hit))
                {
                    if(hit.collider.GetComponentInParent<PlayerWallBlock>() != null)
                    {
                        playerHealth.Hurt();
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
        handler.RemoveFromList(this);
        handler.CheckForEndGame();

        audioSource.pitch = Random.Range(.7f, 1);
        audioSource.PlayOneShot(deathSound[Random.Range(0, deathSound.Length)]);
        EnableRagdoll();
        Destroy(GetComponent<Animator>());
        Destroy(this);
    }
}