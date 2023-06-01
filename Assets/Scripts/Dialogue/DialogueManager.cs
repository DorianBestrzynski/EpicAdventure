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
    [SerializeField] private GameObject choicesBackground;
    [SerializeField] private GameObject scores;
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private Story currentStory; 
    public bool dialogueIsPlaying { get; private set; }
    private bool lastMessage;
    


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
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start() 
    {
        dialogueIsPlaying = false;
        // zakomentowane
        lastMessage = false; 
        dialoguePanel.SetActive(false);
        choicesBackground.SetActive(false);    

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach(GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        //zakomentowane
        if(lastMessage)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Powinno zakończyć dialog.");
                dialogueIsPlaying = false;
                dialoguePanel.SetActive(false);
                dialogueText.text = "";
                lastMessage = false;
                scores.SetActive(true);
            }
        }

        if(!dialogueIsPlaying)
        {
            return;
        }

        if(currentStory.currentChoices.Count == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJSON)
    {
        scores.SetActive(false);
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        // ContinueStory();
        Debug.Log("Rozpoczynamy dialog");
        dialogueText.text = currentStory.currentText;
    }

    private IEnumerator ExitDialogueMode()
    {

        yield return new WaitForSeconds(2f);

        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        // if(currentStory.canContinue)
        // {
        //     dialogueText.text = currentStory.Continue();
        //     DisplayChoices(); 
        // }
        // else
        // {
        //     StartCoroutine(ExitDialogueMode());
        // }

// zakomentowane
        if(currentStory.canContinue)
        {
            dialogueText.text = currentStory.Continue();
            DisplayChoices(); 
        }
        else
        {
            Debug.Log("Ostatnia wiadomość");
            lastMessage = true;
            return;
            // StartCoroutine(ExitDialogueMode());
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " + currentChoices.Count);
        }

        if(currentChoices.Count > 0)
        {
            choicesBackground.SetActive(true);    
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
        choicesBackground.SetActive(false);    
        ContinueStory();
    }
}