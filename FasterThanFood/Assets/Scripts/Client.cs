using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Client : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;

    private TableManager tableManager;
    private Table targetTable;
    private Transform chair;
    public Transform exit;

    private State currentState;

    private Menu menu;
    private Food desiredFood;
    private DrinkType desiredDrink;

    private float tip;
    private float eatTime;

    enum State
    {
        ENTER,
        SIT,
        EAT,
        LEAVE
    }

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        tableManager = FindObjectOfType<TableManager>();
        menu = FindObjectOfType<Menu>();
        currentState = State.ENTER;

        if (tableManager == null)
            Debug.Log("TableManager not found");

        targetTable = tableManager.GetRandomTable();

        //Choose a chair
        if(targetTable != null)
        {
            chair = targetTable.GetChair();
            agent.SetDestination(chair.position);
            ChooseFood();
        }
        else
        {
            Leave();
        }
    }

    void Update()
    {     
        switch (currentState)
        {
            case State.ENTER:
                if (Vector3.Distance(transform.position, chair.position) <= 1.0f)
                {
                    Sit();
                }
                break;

            case State.SIT:
                tip -= Time.deltaTime;
                break;

            case State.EAT:
                eatTime -= Time.deltaTime;
                if (eatTime <= 0)
                    Leave();
                break;
        }

    }

    private void ChooseFood()
    {
        int randomFood = UnityEngine.Random.Range(0, 4);
        int randomDrink = UnityEngine.Random.Range(0, 3);

        desiredFood = menu.FoodTypeToFood((FoodType)randomFood);
        desiredDrink = (DrinkType)randomDrink;
    }

    public Food GetDesiredFood()
    {
        return desiredFood;
    }

    public DrinkType GetDesiredDrink()
    {
        return desiredDrink;
    }

    private void Sit()
    {
        if (targetTable.IsFree())
        {
            agent.isStopped = true;
            currentState = State.SIT;
            tableManager.OccupyTable(targetTable, this);
            transform.position = chair.position;
            transform.rotation = chair.rotation;
            animator.SetTrigger("Sit");
            CalculateTip();
        }
        else
        {
            targetTable = tableManager.GetRandomTable();

            if (targetTable == null)
            {
                Leave();
                return;
            }
        }
    }

    public void StartEating()
    {
        currentState = State.EAT;
        eatTime = UnityEngine.Random.Range(5, 6);
        animator.SetTrigger("Eat");
    }

    public void Leave()
    {
        currentState = State.LEAVE;
        agent.SetDestination(exit.position);
        agent.isStopped = false;
        if(targetTable != null)
        {
            targetTable.FreeTable();
            targetTable.CleanTable();
        }
        animator.SetTrigger("Leave");
    }

    public void SetExit(Transform exitTransform)
    {
        exit = exitTransform;
    }

    private void CalculateTip()
    {
        float rigorousnessLevel = UnityEngine.Random.Range(0, 10);

        tip = desiredFood.GetPreparationTime() + rigorousnessLevel;
    }

    public int GetTip()
    {
        if (tip < 0) tip = 0;
        return Mathf.RoundToInt(tip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Exit"))
        {
            Destroy(this.gameObject);
        }
    }
}
