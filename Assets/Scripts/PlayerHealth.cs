using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Image image;
    private float recoverHealthCooldown = 3;
    private float lastTimeHurt = 0;

    private float[] hurtValues = new float[4] { 0, .01f, .05f, .1f };
    private int hurtIndex = 0;

    private void Update()
    {
        if(Time.time - lastTimeHurt >= recoverHealthCooldown && hurtIndex > 0)
        {
            Recover();
        }
    }

    public void Hurt()
    {
        lastTimeHurt = Time.time;
        hurtIndex = hurtIndex + 1;
        
        if(hurtIndex >= 4)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            UpdateVisuals();
        }
    }

    private void Recover()
    {
        lastTimeHurt = Time.time;
        hurtIndex = Mathf.Max(hurtIndex - 1, 0);
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        Color color = image.color;
        color.a = hurtValues[hurtIndex];
        image.color = color;
    }
}
