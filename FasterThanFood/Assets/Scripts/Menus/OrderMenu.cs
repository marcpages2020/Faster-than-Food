using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderMenu : MonoBehaviour
{
    [SerializeField] float orderTime;
    private float timeLeft;
    [SerializeField] private Text food;
    [SerializeField] private Text drink;

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;

        if(timeLeft <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void ShowCurrentOrder(Table table)
    {
        food.text = table.GetClient().GetDesiredFood().GetFoodType().ToString();
        drink.text = table.GetClient().GetDesiredDrink().ToString();
        timeLeft = orderTime;
    }
}
