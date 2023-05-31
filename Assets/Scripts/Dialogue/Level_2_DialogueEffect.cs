using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_DialogueEffect : MonoBehaviour
{
    public void CorrectAnswer() 
    {
        StaticVariables.hasUnlockedBetterKnight = true;
        StaticVariables.hasUnlockedBetterMagician = true;
    }
}
