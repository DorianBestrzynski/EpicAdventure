using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class CutsceneDialog : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    [SerializeField] private AudioSource mediumSizeVoice;
    [SerializeField] private AudioSource shortSizeVoice;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }

            else
            {
                var isMediumTimeVoice = lines[index].ToCharArray().Length > 40;
                if (isMediumTimeVoice)
                {
                    mediumSizeVoice.Stop();
                }
                else
                {
                    shortSizeVoice.Stop();
                }
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
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
        if(isMediumTimeVoice)
        {
            Debug.Log("Text is done");
            mediumSizeVoice.Stop();
        }
    }

    void NextLine()
    {
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
