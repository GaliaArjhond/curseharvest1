using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // replace with your scene name
    }

    public void OpenSettings()
    {
        Debug.Log("Open settings panel");
        // show/hide a settings panel here
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit called"); // visible in Editor
    }
}