using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Ink.Runtime;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory; 
    public bool dialogueIsPlaying { get; private set; }

    // [Header("Ink JSON")]
    // [SerializeField] private TextAsset inkJSON;
    // public GameObject window;
    // public GameObject indicator;
    // public TMP_Text dialogText;
    // public List<string> dialogs;
    // public float writingSpeed;
    // private int dialogIndex;
    // private int charIndex;
    // private bool started = false;
    // private bool waitForNext = false;
    // private bool p1detected = false;
    // private bool p2detected = false;


    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Found more than one Dialogue Manager in the scene.");
        }
        instance = this;
        // ToggleIndicator(false);
        // ToggleWindow(false);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);    

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void Update()
    {
        if(!dialogueIsPlaying)
            return;

        // if(((p1detected && Input.GetKeyDown(KeyCode.E)) || (p2detected && Input.GetKeyDown(KeyCode.RightShift))))
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    private void ContinueStory()
    {
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices(); 
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        int index = 0;
        foreach(Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }

        for(int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        currentStory.ChooseChoiceIndex(choiceIndex);
    }










    // public void setP1detected(bool isDetected)
    // {
    //     p1detected = isDetected;
    // }

    // public void setP2detected(bool isDetected)
    // {
    //     p2detected = isDetected;
    // }

    // private void ToggleWindow(bool show)
    // {
    //     window.SetActive(show);
    // }

    // public void ToggleIndicator(bool show)
    // {
    //     indicator.SetActive(show);
    // }

    // public void StartDialog() 
    // {
    //     Debug.Log(inkJSON.text);
    //     if(started) 
    //         return;
    //     started = true;
    //     ToggleWindow(true);
    //     ToggleIndicator(false);
    //     GetDialog(0);
    // }

    // private void GetDialog(int i)
    // {
    //     dialogIndex = i;
    //     charIndex = 0;
    //     dialogText.text = string.Empty;
    //     StartCoroutine(Writing());
    // }

    // public void EndDialog()
    // {
    //     started = false;
    //     waitForNext = false;
    //     StopAllCoroutines();
    //     ToggleWindow(false);
    // }

    // IEnumerator Writing()
    // {
    //     yield return new WaitForSeconds(writingSpeed);

    //     string currentDialog = dialogs[dialogIndex];
    //     dialogText.text += currentDialog[charIndex];
    //     charIndex++;
    //     if(charIndex < currentDialog.Length) {
    //         yield return new WaitForSeconds(writingSpeed);
    //         StartCoroutine(Writing());   
    //     }
    //     else 
    //     {
    //         waitForNext = true;
    //     }
    // }
}