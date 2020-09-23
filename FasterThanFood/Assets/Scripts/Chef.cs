using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chef : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private FoodMenu orderMenu;
    [SerializeField] private Image progressionBar;
    [SerializeField] private ParticleSystem particles;

    [Header("Food")]
    [SerializeField] GameObject burger;
    [SerializeField] GameObject pizza;
    [SerializeField] GameObject hotdog;
    [SerializeField] GameObject fries;
    private GameObject currentFoodObject;

    private Animator animator;

    public float cookingTime = 5.0f;
    private float cookingTimeLeft;

    private State currentState;
    private Food currentFood;

    public enum State
    {
        WAITING_FOR_ORDER,
        COOKING,
        WAITING_FOR_PICKUP
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        currentState = State.WAITING_FOR_ORDER;
        currentFood = null;
    }

    void Update()
    {
        if(currentState == State.COOKING)
        {
            cookingTimeLeft -= Time.deltaTime;
            progressionBar.fillAmount = cookingTimeLeft / cookingTime;

            if(cookingTimeLeft <= 0)
            {
                currentState = State.WAITING_FOR_PICKUP;
                animator.SetTrigger("Wave");
                SetFoodActive();
            }
        }
    }

    private void ShowOrderMenu()
    {
        orderMenu.gameObject.SetActive(true);
    }

    public void HideOrderMenu()
    {
        orderMenu.gameObject.SetActive(false);
    }

    public void Cook(Food food)
    {
        currentFood = food;
        cookingTime = food.GetPreparationTime();
        cookingTimeLeft = cookingTime;
        currentState = State.COOKING;
        animator.SetTrigger("Cook");
        particles.gameObject.SetActive(true);
    }

    public void RequestFood()
    {
        if (currentState == State.WAITING_FOR_ORDER)
        {
            ShowOrderMenu();
            gameManager.AssignChef(this);
        }
    }

    public Food GiveFood()
    {
        Food lastPlate = currentFood;
        currentFood = null;

        currentFoodObject.SetActive(false);
        currentFoodObject = null;

        currentState = State.WAITING_FOR_ORDER;
        animator.SetTrigger("Wait");

        return lastPlate;
    }

    public State GetCurrentState()
    {
        return currentState;
    }

    private void SetFoodActive()
    {
       switch (currentFood.GetFoodType())
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

            default:
                currentFoodObject = null;
                break;
        }

        if(currentFoodObject != null)
        {
            currentFoodObject.SetActive(true);
        }

        particles.gameObject.SetActive(false);
    }
}
