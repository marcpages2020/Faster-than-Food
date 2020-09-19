using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Food", menuName = "Consumables/Food", order = 1)]
public class Food : ScriptableObject
{
    [SerializeField] FoodType type;
    [SerializeField] float preparationTime;
    [SerializeField] float price;

    public FoodType GetFoodType()
    {
        return type;
    }

    public float GetPreparationTime()
    {
        return preparationTime;
    }

    public float GetPrice()
    {
        return price;
    }
}

public enum FoodType
{
    BURGER,
    PIZZA,
    HOTDOG,
    FRIES,
    NONE
}
