using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] Food[] foods;
    [SerializeField] Drink[] drinks;

    public Food FoodTypeToFood(FoodType type)
    {
        foreach (Food food in foods)
        {
            if (food.GetFoodType() == type)
            {
                return food;
            }
        }

        Debug.Log("Invalid food");
        return null;
    }

    public Drink DrinkTypeToDrink(DrinkType type)
    {
        foreach (Drink drink in drinks)
        {
            if (drink.GetDrinkType() == type)
            {
                return drink;
            }
        }

        Debug.Log("Invalid drink");
        return null;
    }
}
