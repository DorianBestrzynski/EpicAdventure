using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int pineapplesPlayer1;

    private int pineapplesPlayer2;

    [SerializeField] private Text pineappleText1;

    [SerializeField] private Text pineappleText2;

    [SerializeField] private AudioSource collectionSound;

    [SerializeField] private Transform player;

    private bool wantToInteract;


    private void Start()
    {
        pineapplesPlayer1 = StaticVariables.playerOneCollectibles;
        pineapplesPlayer2 = StaticVariables.playerTwoCollectibles;
        pineappleText1.color = Color.white;
        pineappleText2.color = Color.white;
        pineappleText2.text = $"Pineapples P2:" + pineapplesPlayer2;
        pineappleText1.text = $"Pineapples P1:" + pineapplesPlayer1;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pineapple"))
        {

            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed ) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                pineapplesPlayer2++;
                StaticVariables.playerTwoCollectibles = pineapplesPlayer2;
                pineappleText2.text = $"Pineapples P2:" + pineapplesPlayer2;
                Debug.Log("Player2" + pineapplesPlayer2);
            }
            else
            {
                pineapplesPlayer1++;
                StaticVariables.playerOneCollectibles = pineapplesPlayer1;
                pineappleText1.text = $"Pineapples P1:" + pineapplesPlayer1;
                Debug.Log("Player1" + pineapplesPlayer1);
            }
            collectionSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
