/*using UnityEngine;
using TMPro;

public class NPCName : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject nameBox;
    public TextMeshProUGUI nameText;

    [Header("Dialog Content")]
    [TextArea]
    public string[] nameLines;
    private int currentLineIndex = 0;
    private bool nameActive = false;

    [Header("Journal")]
    public JournalManager journalManager;
    [TextArea]
    public string journalEntry;
    private bool hasAddedToJournal = false;

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!nameActive)
                {
                    StartName();
                }
                else
                {
                    ContinueName();
                }
            }

            if (nameActive && Input.GetKeyDown(KeyCode.Q))
            {
                EndName();
            }
        }
    }

    void StartName()
    {
        currentLineIndex = 0;
        nameActive = true;
        nameBox.SetActive(true);
        nameText.text = nameLines[currentLineIndex];

        if (journalManager != null && !hasAddedToJournal && !string.IsNullOrWhiteSpace(journalEntry))
        {
            journalManager.AddEntry(journalEntry);
            hasAddedToJournal = true;
        }
    }

    void ContinueName()
    {
        currentLineIndex++;

        if (currentLineIndex < nameLines.Length)
        {
            nameText.text = nameLines[currentLineIndex];
        }
        else
        {
            EndName();
        }
    }

    void EndName()
    {
        nameBox.SetActive(false);
        nameActive = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            EndName();
        }
    }
}

*/