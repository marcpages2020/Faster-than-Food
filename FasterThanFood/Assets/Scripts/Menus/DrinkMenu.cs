using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrinkMenu : MonoBehaviour
{
    [SerializeField] Menu menu;
    public GameManager gameManager;

    public Button water;
    public Button tea;
    public Button soda;
    public Button closeMenu;

    private void Start()
    {

        water.onClick.AddListener(() => TakeDrink(menu.DrinkTypeToDrink(DrinkType.WATER)));
        tea.onClick.AddListener(() => TakeDrink(menu.DrinkTypeToDrink(DrinkType.TEA)));
        soda.onClick.AddListener(() => TakeDrink(menu.DrinkTypeToDrink(DrinkType.SODA)));
        closeMenu.onClick.AddListener(() => CloseMenu());
    }

    void TakeDrink(Drink drink)
    {
        gameManager.TakeDrink(drink);
        //Debug.Log("Drink taken: " + drink.GetDrinkType().ToString());
        CloseMenu();
    }

    void CloseMenu()
    {
        this.gameObject.SetActive(false);
    }
}
