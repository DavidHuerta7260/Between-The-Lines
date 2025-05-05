using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    public GameObject journalPanel;
    public GameObject journalNotification;
    public TextMeshProUGUI journalContent;

    private List<string> entries = new List<string>();
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isOpen = !isOpen;
            journalPanel.SetActive(isOpen);

            // Hide notification when opening journal
            if (isOpen && journalNotification != null)
                journalNotification.SetActive(false);
        }

    }

    public void AddEntry(string entry)
    {
        entries.Add(entry);
        RefreshJournal();

        if (journalNotification != null)
            journalNotification.SetActive(true); // Show the notification
    }


    void RefreshJournal()
    {
        journalContent.text = "";
        foreach (string e in entries)
        {
            journalContent.text += "• " + e + "\n\n";
        }
    }
}
