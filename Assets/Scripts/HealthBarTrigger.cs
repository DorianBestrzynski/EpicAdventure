using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarTrigger : MonoBehaviour
{
    [Header("Health bar")]
    [SerializeField] private GameObject healthBar;

    public GameObject test;
   
    private bool playerDetected;
    private bool player1Detected = false;
    private bool player2Detected = false;


    private void Awake() 
    {
        playerDetected = false;
        healthBar.SetActive(false);    
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;
            player1Detected = true;
        }

        if(collision.tag == "Player2")
        {
            playerDetected = true;
            player2Detected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            player1Detected = false;
        }

        if(collision.tag == "Player2")
        {
            player2Detected = false;
        }

        if(!player1Detected && !player2Detected)
        {
            playerDetected = false;
        }
    }

    private void Update()
    {
        if(playerDetected) 
        {
            visualCue.SetActive(true);
        }
        else 
        {
            healthBar.SetActive(false);
        }
    }
}