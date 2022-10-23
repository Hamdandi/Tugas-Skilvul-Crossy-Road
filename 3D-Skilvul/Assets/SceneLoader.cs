using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(int scaneIndex)
    {
        SceneManager.LoadScene(scaneIndex);

        Debug.Log("Load Scene " + scaneIndex);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Akhirnya Selesai, maaf masih gk sempurna masih ada Error hehe " + Application.productName);
    }
}
