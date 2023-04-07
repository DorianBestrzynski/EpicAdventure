using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public class Quiz : MonoBehaviour
{
    public Button button;

    public AudioSource goodSound;

    public AudioSource wrongSound;



    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoodAnswer()
    {
        StaticVariables.playerOneCollectibles += 5;
        StaticVariables.playerTwoCollectibles += 5;
        StaticVariables.playerOneFinalCollectibles = StaticVariables.playerOneCollectibles;
        StaticVariables.playerTwoFinalCollectibles = StaticVariables.playerTwoCollectibles;

        ColorBlock cb = button.colors;
        cb.normalColor = Color.green;
        cb.highlightedColor = Color.green;
        cb.pressedColor = Color.green;
        cb.selectedColor = Color.green;
        button.colors = cb;
        goodSound.Play();
        StartCoroutine(Waiter());
    }

    public void BadAnswer()
    {

        StaticVariables.playerOneCollectibles -= 3;
        StaticVariables.playerTwoCollectibles -= 3;
        StaticVariables.playerOneFinalCollectibles = StaticVariables.playerOneCollectibles;
        StaticVariables.playerTwoFinalCollectibles = StaticVariables.playerTwoCollectibles;

        ColorBlock cb = button.colors;
        cb.normalColor = Color.red;
        cb.highlightedColor = Color.red;
        cb.pressedColor = Color.red;
        cb.selectedColor = Color.red;
        button.colors = cb;
        wrongSound.Play();
        StartCoroutine(Waiter());
    }

    public IEnumerator Waiter()
    {
        yield return new WaitForSeconds(2);
        NextLevel();
    }
}
