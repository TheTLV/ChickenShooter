using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseLevel"); // load màn chơi đầu tiên
    }

    public void OpenSettings(GameObject settingsPanel)
    {
        settingsPanel.SetActive(true); // bật panel Settings
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!"); // chỉ hiện trong editor
    }
}
 