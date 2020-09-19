using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodMenu : MonoBehaviour
{
    [SerializeField] Menu menu;
    public GameManager gameManager;

    public Button burger;
    public Button pizza;
    public Button hotdog;
    public Button fries;
    public Button closeMenu;

    private void Start()
    {
        burger.onClick.AddListener(() => OrderFood(menu.FoodTypeToFood(FoodType.BURGER)));
        pizza.onClick.AddListener(() => OrderFood(menu.FoodTypeToFood(FoodType.PIZZA)));
        hotdog.onClick.AddListener(() => OrderFood(menu.FoodTypeToFood(FoodType.HOTDOG)));
        fries.onClick.AddListener(() => OrderFood(menu.FoodTypeToFood(FoodType.FRIES)));
        closeMenu.onClick.AddListener(() => CloseMenu());
    }

    void OrderFood(Food food)
    {
        //Debug.Log("Food Ordered: " + food.GetFoodType().ToString());
        gameManager.PrepareFood(food);
        CloseMenu();
    }

    void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
