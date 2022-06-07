using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Slider volumeSlider;

    [SerializeField] private Toggle onToggle;



    private float previousSliderValue = 1f;

    void Start()
    {
        
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


  
}
