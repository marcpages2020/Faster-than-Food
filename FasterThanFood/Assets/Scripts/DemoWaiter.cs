using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DemoWaiter : MonoBehaviour
{
    public Transform[] points;
    public GameObject[] foodAndDrink;
    private GameObject currentObject;

    int currentPoint = 0;
    NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);

        currentObject = foodAndDrink[Random.Range(0,foodAndDrink.Length)];
        currentObject.SetActive(true);
    }
    void Update()
    {
        if(Vector3.Distance(transform.position, agent.destination) < 1.5f)
        {
            ChangeDestination();
            ChangeFood();
        }
    }

    private void ChangeDestination()
    {
        if (currentPoint == 0)
        {
            currentPoint = 1;
        }
        else
        {
            currentPoint = 0;
        }

        agent.SetDestination(points[currentPoint].position);
    }

    private void ChangeFood()
    {
        currentObject.SetActive(false);
        currentObject = foodAndDrink[Random.Range(0, foodAndDrink.Length)];
        currentObject.SetActive(true);
    }
}
