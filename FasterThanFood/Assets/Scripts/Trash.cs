using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    private AudioSource throwTrash;

    private void Start()
    {
        throwTrash = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            
            if(player.GetCurrentFood() != null)
            {
                player.DropFood();
                throwTrash.Play(0);
                
            } 
            else if (player.GetCurrentDrink() != null)
            {
                player.DropDrink();
                throwTrash.Play(0);
            }
        }
    }
}
