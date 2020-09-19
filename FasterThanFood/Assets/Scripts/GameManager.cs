using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent<Table> takeOrder;
    [HideInInspector] public UnityEvent timeOver;

    private UIManager uiManager;
    private PlayerController player;
    private TableManager tableManager;
    private Chef orderingChef;

    public float totalScore = 0.0f;
    public float highScore = 0.0f;

    [HideInInspector] public int clientsServed = 0;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (player == null)
            Debug.Log("Player not found");

        uiManager = FindObjectOfType<UIManager>();
        if (uiManager == null)
            Debug.Log("UIManager not found");

        tableManager = FindObjectOfType<TableManager>();
        if (tableManager == null)
            Debug.Log("TableManager not found");

        timeOver.AddListener(TimeOver);

        Time.timeScale = 1.0f;
        highScore = PlayerPrefs.GetFloat("HighScore", 0.0f);
    }

    public void PrepareFood(Food food)
    {
        orderingChef.Cook(food);
    }

    public void TakeDrink(Drink drink)
    {
        player.TakeDrink(drink);
    }

    public void AssignChef(Chef chef)
    {
        orderingChef = chef;
    }

    public void BillRecount(float bill)
    {
        clientsServed++;
        uiManager.UpdateScore(totalScore, bill);
        totalScore += bill;
    }

    public float GetScore()
    {
        return totalScore;
    }

    private void TimeOver()
    {
        if (totalScore > highScore)
        {
            highScore = totalScore;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }
        Time.timeScale = 0;
    }
}
