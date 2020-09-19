using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform exit;

    public float timeBetweenSpawns;
    private float timeLeft = 5;

    private void Start()
    {
        Spawn(RandomObject());
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(RandomObject());
        }
        */

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            Spawn(RandomObject());
        }
    }

    private void Spawn(GameObject objectToSpawn)
    {
        GameObject clone = Instantiate(objectToSpawn, transform.position + Vector3.forward, Quaternion.identity);
        Client client = clone.GetComponent<Client>();
        client.SetExit(exit);

        timeLeft = timeBetweenSpawns;
        //clone.SetActive(true);
    }

    private GameObject RandomObject()
    {
        return prefabs[Random.Range(0, prefabs.Length)];
    }
}
