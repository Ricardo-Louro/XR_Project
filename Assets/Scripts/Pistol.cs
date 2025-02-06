using UnityEngine;

public class Pistol : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip gunshotClip;
    private new ParticleSystem particleSystem;
    private int maxAmmo = 10;
    private int currentAmmo;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        currentAmmo = maxAmmo;
    }

    public void TryShoot()
    {
        if(currentAmmo>=1)
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
        particleSystem.Play();
        audioSource.pitch = Random.Range(.7f, 1);
        audioSource.PlayOneShot(gunshotClip);
    }

    private void NoAmmo()
    {
        Debug.Log("NO BANG!");
    }

    public void Reload()
    {
        currentAmmo = maxAmmo;
    }
}
