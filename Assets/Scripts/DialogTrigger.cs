using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialog dialogScript;    
    private bool playerDetected = false;
    private bool player1Detected = false;
    private bool player2Detected = false;


    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            playerDetected = true;
            player1Detected = true;
            dialogScript.setP1detected(player1Detected);
            // Debug.Log("player 1 detected");
            if(!player2Detected)
            {
                dialogScript.ToggleIndicator(playerDetected);
                // Debug.Log("One of the players is in the range");
            }
        }

        if(collision.tag == "Player2")
        {
            playerDetected = true;
            player2Detected = true;
            dialogScript.setP2detected(player2Detected);
            // Debug.Log("player 2 detected");
            if(!player1Detected)
            {
                dialogScript.ToggleIndicator(playerDetected);
                // Debug.Log("One of the players is in the range");
            }
        }

        if(player1Detected && player2Detected)
        {
            // Debug.Log("Both players are in the range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) 
    {
        if(collision.tag == "Player")
        {
            player1Detected = false;
            dialogScript.setP1detected(player1Detected);
            // Debug.Log("player 1 left");
            if(!player2Detected)
            {
                dialogScript.ToggleIndicator(player1Detected);
                dialogScript.EndDialog();
            }
        }

        if(collision.tag == "Player2")
        {
            player2Detected = false;
            dialogScript.setP2detected(player2Detected);
            // Debug.Log("player 2 left");
            if(!player1Detected)
            {
                dialogScript.ToggleIndicator(player2Detected);
                dialogScript.EndDialog();
            }
        }

        if(!player1Detected && !player2Detected)
        {
            playerDetected = false;
            // Debug.Log("Both players left");
        }
    }

    private void Update()
    {
        if((player1Detected && Input.GetKeyDown(KeyCode.E)) || (player2Detected && Input.GetKeyDown(KeyCode.RightShift)))
        {
            dialogScript.StartDialog();
        }
    }
}
