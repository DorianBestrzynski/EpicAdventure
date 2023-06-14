using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeersSummary : MonoBehaviour
{
    private int beersPlayer1;

    private int beersPlayer2;

    [SerializeField] private TextMeshProUGUI beerText1;

    [SerializeField] private TextMeshProUGUI beerText2;


    private void Start()
    {
        beersPlayer1 = StaticVariables.playerOneFinalCollectibles;
        beersPlayer2 = StaticVariables.playerTwoFinalCollectibles;
        beerText1.color = Color.white;
        beerText2.color = Color.white;
        beerText1.text = $"P1 Piwa:" + beersPlayer1;
        beerText2.text = $"P2 Piwa:" + beersPlayer2;
    }
}
