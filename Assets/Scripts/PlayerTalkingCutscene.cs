using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerTalkingCutscene : MonoBehaviour
{

    public TextMeshProUGUI textComponentP1;
    public TextMeshProUGUI textComponentP2;
    public string[] lines;
    public float textSpeed;
    [SerializeField] private AudioSource mediumSizeVoiceP1;
    [SerializeField] private AudioSource shortSizeVoiceP1;
    [SerializeField] private AudioSource mediumSizeVoiceP2;
    [SerializeField] private AudioSource shortSizeVoiceP2;
    [SerializeField] private GameObject dialogueBg1;
    [SerializeField] private GameObject dialogueBg2;


    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponentP1.text = string.Empty;
        textComponentP2.text = string.Empty;
        StartDialogue();
        dialogueBg1.SetActive(true);
        dialogueBg2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI textComponent;
        if (index % 2 == 0)
        {
            textComponent = textComponentP1;

        }
        else
        {
            textComponent = textComponentP2;

        }

        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }

            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        AudioSource mediumSizeVoice;
        AudioSource shortSizeVoice;
        TextMeshProUGUI textComponent;
        if(index % 2 == 0)
        {
            mediumSizeVoice = mediumSizeVoiceP1;
            textComponent = textComponentP1;
            shortSizeVoice = shortSizeVoiceP1;
        }
        else
        {
            mediumSizeVoice = mediumSizeVoiceP2;
            textComponent = textComponentP2;
            shortSizeVoice = shortSizeVoiceP2;
        }


        mediumSizeVoice.loop = true;
        var isMediumTimeVoice = lines[index].ToCharArray().Length > 40;
        if (isMediumTimeVoice)
        {
            mediumSizeVoice.Play();
        }
        else
        {
            shortSizeVoice.Play();
        }
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        if (isMediumTimeVoice)
        {
            Debug.Log("Text is done");
            mediumSizeVoice.Stop();
        }
    }

    void NextLine()
    {
        dialogueBg1.SetActive(!dialogueBg1.activeSelf);
        dialogueBg2.SetActive(!dialogueBg2.activeSelf);

        TextMeshProUGUI textComponent;
        if (index % 2 == 0)
        {
            textComponent = textComponentP1;

        }
        else
        {
            textComponent = textComponentP2;

        }
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
