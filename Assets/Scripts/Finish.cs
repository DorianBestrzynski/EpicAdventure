using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    private bool knightFinish = false;
    private bool magicianFinish = false;

    private bool levelCompleted = false;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Knight" || collision.gameObject.name == "v2BasicKnight" || collision.gameObject.name == "Magician") && !levelCompleted)
        {
            if(collision.gameObject.name == "Knight" || collision.gameObject.name == "v2BasicKnight")
            {
                knightFinish = true;
            }
            else
            {
                magicianFinish = true;
            }

            if(magicianFinish && knightFinish)
            {
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
            }
        }
    }

        private void OnTriggerExit2D(Collider2D collision)
        {
        if ((collision.gameObject.name == "Knight" || collision.gameObject.name == "v2BasicKnight" || collision.gameObject.name == "Magician") && !levelCompleted)
        {
            if(collision.gameObject.name == "Knight" || collision.gameObject.name == "v2BasicKnight")
            {
                knightFinish = false;
            }
            else
            {
                magicianFinish = false;
            }
        }
    }

    private void CompleteLevel()
    {
        StaticVariables.playerOneFinalCollectibles = StaticVariables.playerOneCollectibles;
        StaticVariables.playerTwoFinalCollectibles = StaticVariables.playerTwoCollectibles;
        StaticVariables.playerOneCollectibles = StaticVariables.playerOneFinalCollectibles;
        StaticVariables.playerTwoCollectibles = StaticVariables.playerTwoFinalCollectibles;
        StaticVariables.playerOneDeathsOnLevel = 0;
        StaticVariables.playerTwoDeathsOnLevel =0;

        if (StaticVariables.is2Levels && SceneManager.GetActiveScene().buildIndex == 7)
        {
            SceneManager.LoadScene(11);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
