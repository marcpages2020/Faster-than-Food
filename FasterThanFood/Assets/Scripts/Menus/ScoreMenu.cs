using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreMenu : MonoBehaviour
{
    [Header ("Text")]
    public Text score;
    public Text clientsServed;
    public Text highScore;

    [Header ("Buttons")]
    public Button playAgain;
    public Button mainMenu;

    public GameManager gameManager;

    private void OnEnable()
    {
        score.text = "Score: " + gameManager.totalScore.ToString() + " $";
        clientsServed.text = "Clients Served: " + gameManager.clientsServed.ToString();

        highScore.text = "High Score: " + gameManager.highScore.ToString() + " $";
    }

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Restaurant");
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
}
