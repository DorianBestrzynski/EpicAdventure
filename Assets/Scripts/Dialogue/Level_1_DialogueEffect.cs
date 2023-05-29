using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_DialogueEffect : MonoBehaviour
{
    [SerializeField] private GameObject PauseMenuButton;
    [SerializeField] private GameObject Choice0;
    [SerializeField] private GameObject Choice1;

    void Start()
    {
        PauseMenuButton.SetActive(false);
    }

    public void FinishDialog()
    {
        PauseMenuButton.SetActive(true);
    }
}
