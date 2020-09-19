using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    public GameManager gameManager;

    public float moveSpeed = 1.0f;
    public float carriedSpeed = 0.5f;
    public float turnSpeed = 20.0f;

    [Header ("Food")]
    [SerializeField] private GameObject burger;
    [SerializeField] private GameObject pizza;
    [SerializeField] private GameObject hotdog;
    [SerializeField] private GameObject fries;

    [Header("Drink")]
    [SerializeField] private GameObject water;
    [SerializeField] private GameObject tea;
    [SerializeField] private GameObject soda;

    private bool isCarrying = false;

    Food currentFood;
    Drink currentDrink;

    Animator m_Animator;
    Rigidbody m_Rigidbody;
    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;

    #endregion

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();

        currentFood = null;
        currentDrink = null;
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        float speed;
        if (isCarrying)
        {
            speed = carriedSpeed;
        }
        else
        {
            speed = moveSpeed;
        }

        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * speed * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Chef")
        {
            Chef chef = collision.gameObject.GetComponent<Chef>();

            if (!isCarrying)
            {
                if (chef.GetCurrentState() == Chef.State.WAITING_FOR_PICKUP)
                {
                    TakeFood(chef);
                }
                else
                {
                    chef.RequestFood();
                }
            }
        }
        else if ((collision.gameObject.CompareTag("Fridge")) && (isCarrying == false))
        {
            Fridge fridge = collision.gameObject.GetComponent<Fridge>();

            fridge.ShowDrinkMenu();            
        }
        else if (collision.gameObject.tag == "Table")
        {
            Table table = collision.gameObject.GetComponent<Table>();

            if (isCarrying == true) 
            {
                if (currentFood != null)
                {
                    if (table.ReceiveFood(currentFood) == true)
                    {
                        DropFood();
                    }
                }
                else if (currentDrink != null)
                {
                    if (table.ReceiveDrink(currentDrink) == true)
                    {
                        DropDrink();
                    }
                }
            } 
            else if (table.ToBeTakenOrder())
            {
                gameManager.takeOrder.Invoke(table);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Chef") == true)
        {
            Chef chef = other.GetComponent<Chef>();
            
            if(chef != null)
            {
                chef.HideOrderMenu();
            }
        }
        else if(other.CompareTag("Fridge") == true)
        {
            Fridge fridge = other.GetComponent<Fridge>();

            if(fridge != null)
            {
                fridge.HideDrinkMenu();
            }
        }
    }

    #region Take/Drop

    void TakeFood(Chef chef)
    {
        isCarrying = true;
        m_Animator.SetBool("IsCarrying", true);
        currentFood = chef.GiveFood();
        ShowFood(currentFood.GetFoodType());

        //Debug.Log("Carrying: " + currentFood.ToString());
    }

    public void DropFood()
    {
        m_Animator.SetBool("IsCarrying", false);
        isCarrying = false;
        HideFood(currentFood.GetFoodType());
        currentFood = null;
    }

    public void TakeDrink(Drink drink)
    {
        isCarrying = true;
        m_Animator.SetBool("IsCarrying", true);

        currentDrink = drink;
        ShowDrink(currentDrink.GetDrinkType());

        //Debug.Log("Carrying: " + currentDrink.ToString());
    }

    public void DropDrink()
    {
        m_Animator.SetBool("IsCarrying", false);
        isCarrying = false;
        HideDrink(currentDrink.GetDrinkType());
        currentDrink = null;
    }

    private void ShowFood(FoodType food)
    {
        switch (food)
        {
            case FoodType.BURGER:
                burger.SetActive(true);
                break;

            case FoodType.PIZZA:
                pizza.SetActive(true);
                break;

            case FoodType.HOTDOG:
                hotdog.SetActive(true);
                break;

            case FoodType.FRIES:
                fries.SetActive(true);
                break;

            default:
                Debug.Log("Trying to show invalid food");
                break;
        }
    }

    private void HideFood(FoodType food)
    {
        switch (food)
        {
            case FoodType.BURGER:
                burger.SetActive(false);
                break;

            case FoodType.PIZZA:
                pizza.SetActive(false);
                break;

            case FoodType.HOTDOG:
                hotdog.SetActive(false);
                break;

            case FoodType.FRIES:
                fries.SetActive(false);
                break;

            default:
                Debug.Log("Trying to hide invalid food");
                break;
        }
    }

    private void ShowDrink(DrinkType drink)
    {
        
        switch (drink)
        {
            case DrinkType.WATER:
                water.SetActive(true);
                break;

            case DrinkType.TEA:
                tea.SetActive(true);
                break;

            case DrinkType.SODA:
                soda.SetActive(true);
                break;

            default:
                Debug.Log("Trying to show invalid drink");
                break;
        }
        
    }

    private void HideDrink(DrinkType drink)
    {
        switch (drink)
        {
            case DrinkType.WATER:
                water.SetActive(false);
                break;

            case DrinkType.TEA:
                tea.SetActive(false);
                break;

            case DrinkType.SODA:
                soda.SetActive(false);
                break;

            default:
                Debug.Log("Trying to hide invalid drink");
                break;
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

    #endregion
}
