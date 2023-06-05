using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_3_DialogueEffect : MonoBehaviour
{
    [SerializeField] private GameObject[] bonusBeers;

    public void Start() 
    {
        int index = 0;
        foreach(GameObject beer in bonusBeers)
        {
            beer.SetActive(false);
            index++;
        }
    }

    public void CorrectAnswer() 
    {
        int index = 0;
        foreach(GameObject beer in bonusBeers)
        {
            beer.SetActive(true);
            index++;
        }
    }
}
