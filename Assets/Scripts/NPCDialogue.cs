using UnityEngine;
using TMPro;
using System.Runtime.InteropServices.WindowsRuntime;

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
    private int currentLineIndex = 0;
    private bool dialogActive = false;
    public string[] nameLines;
    private bool nameActive = false;

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

    public Prompter prompter;

    private bool isAccused = false;

    //private bool waitingDecision = false;



    //for use so that the player can teleport themselves if they reject the initial prompt to
    public bool teloChecker = false;

    void Update()
    {
        if (playerInRange)
        {
//<<<<<<< HEAD
            //getDecision();
            //if (playerInRange && gameObject.CompareTag("Decision") && Input.GetKeyDown(KeyCode.Y) && teloChecker == true)
           //{
                //prompter.teleportPlayer();

                //if (gameObject.name == "Decision Box")
                //{
                  //  prompter.teleportPlayer();
                //}
                //if (gameObject.name == "ReturnBox")
                //{
                //    prompter.teleportPlayer2();
              //  }
///=======
            getDecision();
            if (playerInRange && gameObject.CompareTag("Decision") && Input.GetKeyDown(KeyCode.Y) && teloChecker == true)
           {
                if (gameObject.name == "Decision Box")
                {
                    prompter.teleportPlayer();
                }
                if (gameObject.name == "ReturnBox")
                {
                    prompter.teleportPlayer2();
                }
//>>>>>>> 0250bc7af660a530fd35b742772c81007335123f
 //       


                /* if (Input.GetKeyDown(KeyCode.Y))
                {
                    dialogText.text += "Attention Town's people, I know who the culprit is!";
                    EndDialog();
                    prompter.teleportPlayer();
                    waitingDecision = false;
                }
                else
                {
                    dialogText.text += "No, it's still to early to call it. I can't act rashly now, I've got to be certain I know.";
                    EndDialog();
                    waitingDecision = false;
                }*/


//<<<<<<< HEAD
            //}
//=======
            }
//>>>>>>> 0250bc7af660a530fd35b742772c81007335123f
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

            if (nameActive && dialogActive && Input.GetKeyDown(KeyCode.Q))
            {
                EndDialog();
            }
        }
        if (playerInRange && gameObject.CompareTag("Accusation") && Input.GetKeyDown(KeyCode.Y))
        {
            AccuseNPC();
        }
        

    }

    void StartDialog()
    {
        currentLineIndex = 0;
        dialogActive = true;
        dialogBox.SetActive(true);
        dialogText.text = dialogueLines[currentLineIndex];
        nameActive = true;
        nameBox.SetActive(true);
        nameText.text = nameLines[currentLineIndex];

     //  if (journalManager != null && !hasAddedToJournal && !string.IsNullOrWhiteSpace(journalEntry))
     //   {
     //       journalManager.AddEntry(journalEntry);
     //       hasAddedToJournal = true;
            //prompter.jounalInc();
      //  }
    }

    void ContinueDialog()
    {
        currentLineIndex++;
        ///Debug.Log($"Continuing dialogue: index now {currentLineIndex}/{dialogueLines.Length}");
        //

        if (currentLineIndex < dialogueLines.Length)
        {
            dialogText.text = dialogueLines[currentLineIndex];
            nameText.text = nameLines[0];

           
        }

        else
        {

            EndDialog();

            } 
        }
        
        
    

    public void EndDialog()
    {
        if(journalManager != null && !hasAddedToJournal && !string.IsNullOrWhiteSpace(journalEntry))
        {
            journalManager.AddEntry(journalEntry);
            hasAddedToJournal = true;

            if (journalManager.entryCount >= 5 && !prompter.hasPrompted) {
                prompter.StartPrompter();
                prompter.hasPrompted = true;
            }
           
           // prompter.jounalInc();
        }
        dialogBox.SetActive(false);
        dialogActive = false;
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
            EndDialog();
        }
    }

    void AccuseNPC()
    {

        if (isAccused) return;
        isAccused = true;


        dialogText.text = $"{nameText.text} has been accused!";
        if (isCorrectAccused) {
            dialogText.text += "\nYou accused correctly. Justice has been served!";
            Application.Quit();
        }
        else
        {
            dialogText.text += "\nThat was incorrect. The real culprit is still out there.";
            Application.Quit();

        }

        dialogActive = false;

        

    }
    //sets setdecision to true, this is so that that the player cannot
    //teleport themselves early
//<<<<<<< HEAD
   // public void setDecision() {
   //     teloChecker = true;
        
  //  }

  //  bool getDecision() {
   //     return teloChecker;
   // }
//=======
    public void setDecision() {
        teloChecker = true;
        
    }

    bool getDecision() {
        return teloChecker;
    }
//>>>>>>> 0250bc7af660a530fd35b742772c81007335123f
   /* void DecisionTelo()
    {
        if (isDecision) return;
        isDecision = true;



        if (isDecision)
        {
            

        }
    }*/



    // if (nameText.text == "John") {

    //}

}
 


