using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject journalPanel;
    public GameObject journalButton;
    public GameObject journalNotification;
    public TextMeshProUGUI journalText;

    [Header("External References")]
    public Prompter prompter;

    private List<string> entries = new List<string>();
    private bool isOpen = false;
    public int entryCount = 0;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
        }
    }

    public void ToggleJournal()
    {
        isOpen = !isOpen;
        journalPanel.SetActive(isOpen);

        if (journalButton != null)
        {
            journalButton.SetActive(!isOpen);
        }

        if (isOpen && journalNotification != null)
        {
            journalNotification.SetActive(false); // hide notification on open
        }
    }

    public void AddEntry(string entry)
    {
        if (string.IsNullOrWhiteSpace(entry)) return;

        entries.Add(entry);
        entryCount++;
        RefreshJournal();

        if (!isOpen && journalNotification != null)
        {
            journalNotification.SetActive(true); // show notification only if journal is closed
        }

        if (prompter != null)
        {
            prompter.jounalInc(); // notify Prompter script of journal update
        }
    }

    public void ClearJournal()
    {
        entries.Clear();
        entryCount = 0;
        journalText.text = "";
    }

    private void RefreshJournal()
    {
        journalText.text = "";
        foreach (string e in entries)
        {
            journalText.text += "• " + e + "\n\n";
        }
    }
}
