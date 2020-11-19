using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region ---Variables---
    private Button playButton;
    private Button exitButton;
    #endregion

    #region -- ButtonAssign--
    public void StartGame()
    {
        SceneManager.LoadScene(1);
        Debug.LogWarning("True");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
}
