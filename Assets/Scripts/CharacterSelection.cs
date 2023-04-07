using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour
{

    [SerializeField] private Toggle p1Magician;

    [SerializeField] private Toggle p1Knight;

    [SerializeField] private Toggle p2Magician;

    [SerializeField] private Toggle p2Knight;

    [SerializeField] private Toggle diffEasy;

    [SerializeField] private Toggle diffHard;

    [SerializeField] private Toggle levels2;

    [SerializeField] private Toggle levels4;
    // Start is called before the first frame update


    void Update()
    {
    }
    public void StartGame()
    {
        StaticVariables.isPLayerOneMagician = p1Magician.isOn;
        StaticVariables.isPLayerTwoMagician = p2Magician.isOn;
        StaticVariables.isEasyMode = diffEasy.isOn;
        StaticVariables.is2Levels = levels2.isOn;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleChange(bool isOn)
    {
        if (transform.gameObject.name == "Magician" && isOn)
        {
            if (p2Magician.isOn)
            {
                p2Magician.isOn = false;
                p2Knight.isOn = true;
            }
        }
        else if (transform.gameObject.name == "Knight" && isOn)
        {
            if (p2Knight.isOn)
            {
                p2Knight.isOn = false;
                p2Magician.isOn = true;
            }
        }

        else if (transform.gameObject.name == "MagicianP2" && isOn)
        {
            if (p1Magician.isOn)
            {
                p1Magician.isOn = false;
                p1Knight.isOn = true;
            }
        }

        else if (transform.gameObject.name == "KnightP2" && isOn)
        {
            if (p1Knight.isOn)
            {
                p1Knight.isOn = false;
                p1Magician.isOn = true;
            }
        }
    }

}
