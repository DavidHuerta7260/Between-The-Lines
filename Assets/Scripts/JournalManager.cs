using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class JournalManager : MonoBehaviour
{
    public GameObject journalPanel;
    public TextMeshProUGUI journalContent;

    private List<string> entries = new List<string>();
    private bool isOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isOpen = !isOpen;
            journalPanel.SetActive(isOpen);
        }
    }

    public void AddEntry(string entry)
    {
        entries.Add(entry);
        RefreshJournal();
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
