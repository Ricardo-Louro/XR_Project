using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] private AudioSource gunshot_audioSource;
    private new ParticleSystem particleSystem;
    private int maxAmmo = 10;
    private int currentAmmo;

    private void Start()
    {
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
        gunshot_audioSource.pitch = Random.Range(.8f,1);
        gunshot_audioSource.Play();
        Debug.Log("BANG!");
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
