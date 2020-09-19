using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    [SerializeField] private DrinkMenu drinkMenu;

    public void ShowDrinkMenu()
    {
        drinkMenu.gameObject.SetActive(true);
    }

    public void HideDrinkMenu()
    {
        drinkMenu.gameObject.SetActive(false);
    }
}
