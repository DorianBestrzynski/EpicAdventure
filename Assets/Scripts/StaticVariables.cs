using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticVariables : MonoBehaviour 
{
    public static bool isDead = false;

    public static int playerOneCollectibles = 0; 

    public static int playerTwoCollectibles = 0;

    public static int playerOneFinalCollectibles = 0;

    public static int playerTwoFinalCollectibles = 0;

    public static int playerOneLife = -1;

    public static int playerTwoLife = -1;

    public static int bossLife = 40;

    public static float playerOneSpeed;

    public static float playerTwoSpeed;

    public static bool isPLayerOneMagician = false;

    public static bool isPLayerTwoMagician = true;

    public static bool isEasyMode;

    public static bool is2Levels;

    public static float playerOneJumpForce;

    public static float playerTwoJumpForce;

    public static bool hasSwithed = false;

    public bool finalLevel = false;

    public static bool hasUnlockedBetterKnight = false;

    public static bool hasUnlockedBetterMagician = false;

    public static bool hasRestartedLevel = false;

    public static int playerOneDeathsOnLevel = 0;

    public static int playerTwoDeathsOnLevel = 0;

    public static float savedVolume = 1;
    public static float savedSliderValue = 1;
}
