using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject controlsPanel;
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Controls()
    {
        controlsPanel.SetActive(true);

    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Back()
    {
        controlsPanel.SetActive(false);
    }
}
