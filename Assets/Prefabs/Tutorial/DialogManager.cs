using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour
{
    public TMP_Text textbox;
    public string[] sentences;
    private int index;
    public float typingSpeed;

    public GameObject continueButton;
    public GameObject dialogPanel;
    public JournalManager journalManager; // Drag this in the Inspector

    private void OnEnable()
    {
        continueButton.SetActive(false);
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        textbox.text = " ";
        foreach (char letter in sentences[index])
        {
            textbox.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        continueButton.SetActive(true);
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textbox.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textbox.text = " ";
            dialogPanel.SetActive(false);

            // ✅ Enable journal button/icon after tutorial ends
            if (journalManager != null && journalManager.journalButton != null)
            {
                journalManager.journalButton.SetActive(true);
            }
        }
    }

    void Update()
    {
        // No update logic required for now
    }
}
