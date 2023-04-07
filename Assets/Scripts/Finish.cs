using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    private AudioSource finishSound;

    private bool levelCompleted = false;
    // Start is called before the first frame update
    private void Start()
    {
        finishSound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.name == "Knight" || collision.gameObject.name == "Magician") && !levelCompleted)
        {
            finishSound.Play();
            levelCompleted = true;
            Invoke("CompleteLevel", 2f);
        }
        Debug.Log("lol");
    }

    private void CompleteLevel()
    {
        StaticVariables.playerOneFinalCollectibles = StaticVariables.playerOneCollectibles;
        StaticVariables.playerTwoFinalCollectibles = StaticVariables.playerTwoCollectibles;
        StaticVariables.playerOneCollectibles = StaticVariables.playerOneFinalCollectibles;
        StaticVariables.playerTwoCollectibles = StaticVariables.playerTwoFinalCollectibles;

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
