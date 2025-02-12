using UnityEngine;

public class Pistol : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip[] gunshotClips;
    [SerializeField] private AudioClip emptyClip;
    [SerializeField] private AudioClip insertClip;
    private AudioSource audioSource;

    [Header("Shoot")]
    [SerializeField] private Transform firingTransform;
    [SerializeField] private int gunshotForce;

    [Header("VFX")]
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private ParticleSystem hitEffect;

    private Transform pistolTransform;
    private Animator animator;
    private bool held = false;
    private int maxAmmo = 10;
    private int currentAmmo = 0;

    private void Start()
    {
        pistolTransform = FindFirstObjectByType<PistolPosition>().transform;
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!held)
        {
            UpdatePosition();
        }
    }

    public void TryShoot()
    {
        if (currentAmmo >= 1)
        {
            SuccessfulShoot();
        }
        else
        {
            NoAmmo();
        }
    }

    private void SuccessfulShoot()
    {
        currentAmmo--;
        animator.SetTrigger("Shoot");
        muzzleFlash.Play();
        PlaySound(gunshotClips[Random.Range(0, gunshotClips.Length)]);

        RaycastHit hit;
        if (Physics.Raycast(firingTransform.position, firingTransform.forward, out hit))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if (enemy != null)
            {
                hit.collider.enabled = false;
                enemy.EnableRagdoll();
            }
            else
            {
                hitEffect.transform.position = hit.point;
                hitEffect.transform.forward = firingTransform.position - hit.point;
                hitEffect.Play();
            }

            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(hit.transform.position + (firingTransform.forward * gunshotForce), ForceMode.Force);
            }
        }
    }

    private void UpdatePosition()
    {
        transform.position = pistolTransform.position;
        transform.rotation = pistolTransform.rotation;
    }

    private void NoAmmo()
    {
        PlaySound(emptyClip);
    }

    public void Reload()
    {
        PlaySound(insertClip);
        currentAmmo = maxAmmo;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.pitch = Random.Range(.7f, 1);
        audioSource.PlayOneShot(clip);
    }

    public void Held()
    {
        held = true;
    }

    public void Release()
    {
        held = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        PistolClip pistolClip = other.GetComponent<Collider>().GetComponent<PistolClip>();

        if (pistolClip != null)
        {
            if (pistolClip.CanReload)
            {
                Destroy(other.gameObject);
                Reload();
            }
        }
    }
}