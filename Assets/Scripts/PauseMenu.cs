using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle onToggle;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject instructionsMenu;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject scores;


    private float previousSliderValue = 1f;

    void Start()
    {
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(false);
        AudioListener.volume = 1;  
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ChangeVolumne()
    {
        AudioListener.volume = volumeSlider.value;
        if (volumeSlider.value == 0)
        {
            onToggle.isOn = false;
        }
        else if (previousSliderValue == 0 && volumeSlider.value > 0)
        {
            onToggle.isOn = true;
        }
        previousSliderValue = volumeSlider.value;
    }

    public void ToggleAudioOnValueChange(bool audioIn)
    {
        if (!audioIn)
        {
            Debug.Log("Should turn off" + audioIn);
            AudioListener.volume = 0;
            volumeSlider.value = 0;
        }
        else if (audioIn)
        {
            Debug.Log("Should turn on" + audioIn);
            AudioListener.volume = 1;
            volumeSlider.value = 1;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        scores.SetActive(false);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
        scores.SetActive(true);
    }

    public void OpenInstructionsMenu()
    {
        pauseMenu.SetActive(false);
        instructionsMenu.SetActive(true);
    }

    public void CloseInstructionsMenu()
    {
        instructionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
