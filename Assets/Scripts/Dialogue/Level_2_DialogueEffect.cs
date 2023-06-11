using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_DialogueEffect : MonoBehaviour
{

    private void Start()
    {
        StaticVariables.hasUnlockedBetterKnight = false;
        StaticVariables.hasUnlockedBetterMagician = false;  
    }

    public void CorrectAnswer() 
    {
        StaticVariables.hasUnlockedBetterKnight = true;
        StaticVariables.hasUnlockedBetterMagician = true;
    }
}
