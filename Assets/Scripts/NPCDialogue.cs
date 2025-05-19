using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject dialogBox;
    public TextMeshProUGUI dialogText;
    public GameObject nameBox;
    public TextMeshProUGUI nameText;

    [Header("Dialog Content")]
    [TextArea]
    public string[] dialogueLines;
    public string[] nameLines;
    [TextArea]
    public string questionPrompt;
    [TextArea]
    public string response1;
    [TextArea]
    public string response2;
    [TextArea]
    public string response3;

    [Header("Journal")]
    public JournalManager journalManager;
    [TextArea]
    public string journalEntry;
    private bool hasAddedToJournal = false;

    [Header("Accusation")]
    public bool isCorrectAccused = false;

    [Header("Decision")]
    public bool isDecision = false;

    private bool playerInRange = false;
    private bool dialogActive = false;
    private bool nameActive = false;
    private bool isAccused = false;
    private bool awaitingQuestion = false;
    private bool waitingDecision = false;

    public Prompter prompter;
    public bool teloChecker = false; // for teleport trigger

    private bool showRestartPrompt = false;
    private int currentLineIndex = 0;

    void Update()
    {
        if (playerInRange)
        {
            if (gameObject.CompareTag("Decision") && Input.GetKeyDown(KeyCode.Y) && teloChecker)
            {
                if (gameObject.name == "Decision Box")
                {
                    prompter.teleportPlayer();
                }
                else if (gameObject.name == "ReturnBox")
                {
                    prompter.teleportPlayer2();
                }
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!dialogActive)
                    StartDialog();
                else if (!awaitingQuestion)
                    ContinueDialog();
            }

            if (awaitingQuestion)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    dialogText.text = response1;
                    awaitingQuestion = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    dialogText.text = response2;
                    awaitingQuestion = false;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    dialogText.text = response3;
                    awaitingQuestion = false;
                }
            }

            if (dialogActive && Input.GetKeyDown(KeyCode.Q))
            {
                EndDialog();
            }
        }

        if (playerInRange && gameObject.CompareTag("Accusation") && Input.GetKeyDown(KeyCode.Y))
        {
            AccuseNPC();
        }

        if (showRestartPrompt && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    void StartDialog()
    {
        dialogActive = true;
        dialogBox.SetActive(true);
        nameBox.SetActive(true);
        nameActive = true;
        nameText.text = nameLines.Length > 0 ? nameLines[0] : gameObject.name;

        currentLineIndex = 0;
        dialogText.text = dialogueLines[currentLineIndex];

        if (journalManager != null && !hasAddedToJournal && !string.IsNullOrWhiteSpace(journalEntry))
        {
            journalManager.AddEntry(journalEntry);
            hasAddedToJournal = true;

            prompter.jounalInc();

            if (journalManager.entryCount >= 5 && !prompter.hasPrompted)
            {
                prompter.StartPrompter();
                dialogText.text = "It seems we have enough evidence to proceed with the Trial? \nYes or No \n(On the Keyboard Hit Y for yes and N for no)";
                prompter.hasPrompted = true;
            }
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
            dialogText.text = questionPrompt;
            awaitingQuestion = true;
        }
    }

    public void EndDialog()
    {
        dialogBox.SetActive(false);
        dialogActive = false;
        nameBox.SetActive(false);
        nameActive = false;
        awaitingQuestion = false;
        currentLineIndex = 0;
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
            if (dialogActive)
            {
                EndDialog();
            }
        }
    }

    void AccuseNPC()
    {
        if (isAccused) return;
        isAccused = true;

        dialogBox.SetActive(true);
        dialogActive = true;
        nameBox.SetActive(false);

        dialogText.text = $"{nameText.text} has been accused!";
        if (isCorrectAccused)
        {
            dialogText.text += "\nYou accused correctly. Justice has been served!";
        }
        else
        {
            dialogText.text += "\nThat was incorrect. The real culprit is still out there.";
        }

        dialogText.text += "\n\nPress 'R' to return to the title screen.";
        dialogActive = false;
        showRestartPrompt = true;
    }

    public void setDecision()
    {
        teloChecker = true;
    }

    bool getDecision()
    {
        return teloChecker;
    }
}
