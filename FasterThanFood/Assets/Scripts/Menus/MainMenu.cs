using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame()
    {
        SceneManager.LoadScene("Restaurant");
    }

    public void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }

    public void CloseMenu(GameObject closingMenu)
    {
        closingMenu.SetActive(false);
    }
    public void OpenMenu(GameObject openingMenu)
    {
        openingMenu.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
