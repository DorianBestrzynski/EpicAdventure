using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int beersPlayer1;

    private int beersPlayer2;

    [SerializeField] private Text beerText1;

    [SerializeField] private Text beerText2;

    [SerializeField] private AudioSource collectionSound;

    [SerializeField] private Transform player;

    private bool wantToInteract;


    private void Start()
    {
        beersPlayer1 = StaticVariables.playerOneCollectibles;
        beersPlayer2 = StaticVariables.playerTwoCollectibles;
        beerText1.color = Color.white;
        beerText2.color = Color.white;
        beerText2.text = $"Beers:" + beersPlayer2;
        beerText1.text = $"Beers:" + beersPlayer1;
        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Beer"))
        {

            if ((player.gameObject.CompareTag("Player2") && !StaticVariables.hasSwithed ) || (transform.gameObject.CompareTag("Player") && StaticVariables.hasSwithed))
            {
                beersPlayer2++;
                StaticVariables.playerTwoCollectibles = beersPlayer2;
                beerText2.text = $"Beers:" + beersPlayer2;
                Debug.Log("Player2" + beersPlayer2);
            }
            else
            {
                beersPlayer1++;
                StaticVariables.playerOneCollectibles = beersPlayer1;
                beerText1.text = $"Beers:" + beersPlayer1;
                Debug.Log("Player1" + beersPlayer1);
            }
            collectionSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
