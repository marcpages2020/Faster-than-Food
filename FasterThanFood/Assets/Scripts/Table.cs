using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    #region Variables

    GameManager gameManager;
    TableManager tableManager;

    [SerializeField] ParticleSystem particles;

    [SerializeField] private GameObject plate;

    [Header("Food")]
    [SerializeField] private GameObject burger;
    [SerializeField] private GameObject pizza;
    [SerializeField] private GameObject hotdog;
    [SerializeField] private GameObject fries;

    [Header("Drink")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject tea;
    [SerializeField] private GameObject soda;

    [Header("Audio")]
    [SerializeField] private AudioSource plateSound;
    [SerializeField] private AudioSource bottleSound;

    [Header("Sprites")]
    [SerializeField] private Animator thumbUp;
    [SerializeField] private Animator thumbDown;

    [SerializeField] private GameObject[] chairs;

    private GameObject currentFoodObject;
    private GameObject currentDrinkObject;

    private Food currentFood;
    private Drink currentDrink;

    private Client currentClient = null;

    private bool isFree = true;
    private bool orderTaken = false;

    #endregion

    void Start()
    {
        currentFood = null;
        currentDrink = null;

        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
            Debug.Log("GameManager not found");

        gameManager.takeOrder.AddListener(TakeOrder);

        tableManager = FindObjectOfType<TableManager>();
        if (tableManager == null)
            Debug.Log("TableManager not found");
    }

    #region Food and Drink
    public bool ReceiveFood(Food food)
    {
        if ((currentFood == null) && (currentClient != null))
        {
            switch (food.GetFoodType())
            {
                case FoodType.BURGER:
                    currentFoodObject = burger;
                    break;

                case FoodType.PIZZA:
                    currentFoodObject = pizza;
                    break;

                case FoodType.HOTDOG:
                    currentFoodObject = hotdog;
                    break;

                case FoodType.FRIES:
                    currentFoodObject = fries;
                    break;
            }

            currentFood = food;
            currentFoodObject.SetActive(true);
            plate.SetActive(true);
            plateSound.Play();

            if (currentFood.GetFoodType() == currentClient.GetDesiredFood().GetFoodType())
            {
                thumbUp.Play("thumbs");
            }
            else
            {
                thumbDown.Play("thumbs");
            }

            if (currentDrink != null)
            {
                CheckOrder();
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public bool ReceiveDrink(Drink drink)
    {
        if ((currentDrink == null) && (currentClient != null))
        {
            switch (drink.GetDrinkType())
            {
                case DrinkType.WATER:
                    currentDrinkObject = water;
                    break;
                case DrinkType.TEA:
                    currentDrinkObject = tea;
                    break;
                case DrinkType.SODA:
                    currentDrinkObject = soda;
                    break;
                
            }

            currentDrink = drink;
            currentDrinkObject.SetActive(true);
            bottleSound.Play();

            if (currentDrink.GetDrinkType() == currentClient.GetDesiredDrink())
            {
                thumbUp.Play("thumbs");
            }
            else
            {
                thumbDown.Play("thumbs");
            }

            if (currentFood != null)
            {
                CheckOrder();
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    public Food GetCurrentFood()
    {
        return currentFood;
    }

    public Drink GetCurrentDrink()
    {
        return currentDrink;
    }

    public Client GetClient()
    {
        return currentClient;
    }

    private void CheckOrder()
    {
        float bill = 0.0f; 
        
        if(currentFood.GetFoodType() == currentClient.GetDesiredFood().GetFoodType())
        {
            bill += currentFood.GetPrice();
        }

        if(currentDrink.GetDrinkType() == currentClient.GetDesiredDrink())
        {
            bill += currentDrink.GetPrice();
        }

        if(bill > 0)
        {
            bill += currentClient.GetTip();
            currentClient.StartEating();
            gameManager.BillRecount(bill);
        }
        else
        {
            Debug.Log("Bad Order");
        }
    }

    #endregion

    public bool IsFree()
    {
        return isFree;
    }

    public void OccupyTable(Client client)
    {
        currentClient = client;
        isFree = false;
    }

    public void FreeTable()
    {
        currentClient = null;
        isFree = true;
        orderTaken = false;
    }

    public void CleanTable()
    {
        currentFoodObject.SetActive(false);
        currentFood = null;

        currentDrinkObject.SetActive(false);
        currentDrinkObject = null;

        plate.SetActive(false);

        particles.gameObject.SetActive(true);
    }

    public Transform GetChair()
    {
        int chairNumber = Random.Range(0, chairs.Length);
        return chairs[chairNumber].transform;
    }

    public bool ToBeTakenOrder()
    {
        return (orderTaken == false) && (currentClient != null); 
    }

    private void TakeOrder(Table table)
    {
        if(table == this)
        {
            orderTaken = true;
        }
    }
}

