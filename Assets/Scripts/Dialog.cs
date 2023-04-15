using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public GameObject window;
    public GameObject indicator;
    public TMP_Text dialogText;
    public List<string> dialogs;
    public float writingSpeed;
    private int dialogIndex;
    private int charIndex;
    private bool started = false;
    private bool waitForNext = false;
    private bool p1detected = false;
    private bool p2detected = false;

    public void setP1detected(bool isDetected)
    {
        p1detected = isDetected;
    }

    public void setP2detected(bool isDetected)
    {
        p2detected = isDetected;
    }

    private void Awake()
    {
        ToggleIndicator(false);
        ToggleWindow(false);
    }

    private void ToggleWindow(bool show)
    {
        window.SetActive(show);
    }

    public void ToggleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    public void StartDialog() 
    {
        if(started) 
            return;
        started = true;
        ToggleWindow(true);
        ToggleIndicator(false);
        GetDialog(0);
    }

    private void GetDialog(int i)
    {
        dialogIndex = i;
        charIndex = 0;
        dialogText.text = string.Empty;
        StartCoroutine(Writing());
    }

    public void EndDialog()
    {
        started = false;
        waitForNext = false;
        StopAllCoroutines();
        ToggleWindow(false);
    }

    IEnumerator Writing()
    {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialog = dialogs[dialogIndex];
        dialogText.text += currentDialog[charIndex];
        charIndex++;
        if(charIndex < currentDialog.Length) {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(Writing());   
        }
        else 
        {
            waitForNext = true;
        }
    }

    private void Update()
    {
        if(!started)
            return;

        if(waitForNext && ((p1detected && Input.GetKeyDown(KeyCode.E)) || (p2detected && Input.GetKeyDown(KeyCode.RightShift))))
        {
            waitForNext = false;
            dialogIndex++;

            if(dialogIndex < dialogs.Count)
            {
                GetDialog(dialogIndex);
            }
            else
            {
                ToggleIndicator(true);
                EndDialog();
            }
        }
    }
}