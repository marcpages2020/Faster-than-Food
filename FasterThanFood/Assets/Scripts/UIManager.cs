using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    [HideInInspector] public UnityEvent<Food> prepareFood;
    [SerializeField] PauseMenu pauseMenu;
    [SerializeField] OrderMenu orderMenu;
    [SerializeField] Text scoreText;
    public ScoreMenu scoreMenu;

    [SerializeField] private float updateScoreTime;
    private float timeLeft;
    private bool updateTime = false;
    //public Animator scoreAnimator; 

    void Start()
    {
        prepareFood.AddListener(gameManager.PrepareFood);
        gameManager.takeOrder.AddListener(ShowOrderMenu);
        gameManager.timeOver.AddListener(TimeOver);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenu.ToggleMenu();
        }

        if (updateTime)
        {
            timeLeft -= Time.deltaTime;
            if(timeLeft <= 0)
            {
                updateTime = false;
                scoreText.text = gameManager.GetScore().ToString() + " $";
                scoreText.color = Color.white;
                //scoreAnimator.Play("ScoreAdded");
            }
        }
    }

    public void OrderFood(Food food)
    {
        prepareFood.Invoke(food);
    }

    public void ShowOrderMenu(Table table)
    {
        orderMenu.gameObject.SetActive(true);
        orderMenu.ShowCurrentOrder(table);
    }

    public void TimeOver()
    {
        scoreMenu.gameObject.SetActive(true);
    }

    public void UpdateScore(float score, float bill)
    {
        timeLeft = updateScoreTime;
        updateTime = true;
        scoreText.text = score.ToString() + " + " + bill.ToString() + " $";
        scoreText.color = Color.green;
    }
}
