using UnityEngine;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;

    [Header("Dialog Content")]
    [TextArea]
    public string[] dialogueLines;
    private int currentLineIndex = 0;
    private bool dialogActive = false;

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
                if (!dialogActive)
                {
                    StartDialog();
                }
                else
                {
                    ContinueDialog();
                }
            }

            if (dialogActive && Input.GetKeyDown(KeyCode.Q))
            {
                EndDialog();
            }
        }
    }

    void StartDialog()
    {
        currentLineIndex = 0;
        dialogActive = true;
        dialogBox.SetActive(true);
        dialogText.text = dialogueLines[currentLineIndex];

        if (journalManager != null && !hasAddedToJournal && !string.IsNullOrWhiteSpace(journalEntry))
        {
            journalManager.AddEntry(journalEntry);
            hasAddedToJournal = true;
        }
    }

    void ContinueDialog()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            dialogText.text = dialogueLines[currentLineIndex];
        }
        else
        {
            EndDialog();
        }
    }

    void EndDialog()
    {
        dialogBox.SetActive(false);
        dialogActive = false;
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
            EndDialog();
        }
    }
}

