using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        CloseMenu();
    }

    public void ToggleMenu()
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (gameObject.activeSelf == false)
            Time.timeScale = 1.0f;
        else
            Time.timeScale = 0.0f;
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Restaurant");
    }

    public void QuitGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
