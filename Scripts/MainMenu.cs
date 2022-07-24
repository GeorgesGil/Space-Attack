using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene;

    public void StartGame()
    {
        AudioManager.instance.PlaySFX(3);
        SceneManager.LoadScene(startScene);
    }

    public void OnApplicationQuit()
    {
        AudioManager.instance.PlaySFX(3);
        Application.Quit();
    }
}
