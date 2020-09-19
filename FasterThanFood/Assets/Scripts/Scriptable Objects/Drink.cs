using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Drink", menuName = "Consumables/Drink", order = 1)]
public class Drink : ScriptableObject
{
    [SerializeField] DrinkType type;
    [SerializeField] float price;

    public DrinkType GetDrinkType()
    {
        return type;
    }

    public float GetPrice()
    {
        return price;
    }
}

public enum DrinkType
{
    WATER,
    TEA,
    SODA,
    NONE
}
