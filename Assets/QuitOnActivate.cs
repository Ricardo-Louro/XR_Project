using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitOnActivate : MonoBehaviour
{
    private void Start()
    {
        SceneManager.LoadScene(0);
    }
}