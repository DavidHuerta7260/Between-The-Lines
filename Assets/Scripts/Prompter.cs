using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;

public class Prompter : MonoBehaviour
{
    public PlayerController player;
    public Transform teleportTarget;

    public Transform teleportTarget2;
    // this is for when the player has made 5 journal entries then the player
    //will be prompted if they want to go ahead with the trial
    public GameObject prompterBox;
    public TextMeshProUGUI prompterContent;

    public string[] prompterLines;
  //  private int currentLineIndex = 0;
    private bool prompterActive = false;
    private bool hasPrompted = false;

    public GameObject hider;

    public NPCDialogue npcDialogue;

    public Transform DecisionSpawn1prefab;
    public Transform DecisionSpawn2prefab;

    public GameObject DecisionArea1;
    public GameObject DecisionArea2;

    //public NPCDialogue npcDialogue;

    //bool reject = false;
    static int factor = 0;

    private bool decisionAreaSpawned;
    //waiting time for the player gives player time to talk to final npc and
    //time to think on their own
    public float delay = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //takes the journalInc for npc dialogue


        if (factor == 5 && !hasPrompted) {
            hasPrompted=true;
            StartCoroutine(PromptAfterDelay());
        }
        if (prompterActive)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                //teleportPlayer();
                player.transform.position = teleportTarget.position;
                EndPrompter();

            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                prompterContent.text = "When you're ready go back to the center of town and press Y. again.";
               // npcDialogue.setDecision();
                EndPrompter();

                //spawn points for decision boxes on level 1 and level 2
                if (!decisionAreaSpawned)
                {

                    DecisionArea1.transform.position = DecisionSpawn1prefab.position;
                    DecisionArea2.transform.position = DecisionSpawn2prefab.position;

                   // Instantiate(DecisionArea2, DecisionSpawn2prefab.transform.position, Quaternion.identity);
                    decisionAreaSpawned = true;
                }

            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                teleportPlayer();
            }

        }
        if (factor == 10 && !hasPrompted)
        {
            hasPrompted = true;
            StartCoroutine(PromptAfterDelay());
        }

    }

    void StartPrompter() {
        prompterBox.SetActive(true);

        if (factor == 5) {
            prompterContent.text = "It seems we have enought evidence to procced with the Trial? \nYes or No \n(On the Keyboard Hit Y for yes and N for no)";
        }
       // if (factor == 10)
       // {
        //    prompterContent.text = "I suspect the cultprit is!";
       // }
        prompterActive =true;

        
        // currentLineIndex = 0;
        //prompterActive = true;
        //   prompterBox.SetActive(true);
        // prompterContent.text = prompterLines[currentLineIndex];
    }

    void EndPrompter()
    {
        prompterActive = false;
        StartCoroutine(HidePrompterAfterDelay(2f));

        
 
    }

    public void jounalInc() {
        factor++;   
    }

    IEnumerator HidePrompterAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        prompterBox.SetActive(false);
    }


    IEnumerator PromptAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        StartPrompter();
    }
    ///// void teleport() { 

    public void teleportPlayer() {
        player.enabled = false;

            player.transform.position = teleportTarget.position;
        
       

        player.enabled = true;
    }

    public void teleportPlayer2()
    {
        player.enabled = false;

            player.transform.position = teleportTarget2.position;
    

        player.enabled = true;
    }

    //this will spawn a black object infront of the camera and will obscure the player's and scene as
    //the player is teleported, will dissapear not to long after
    public void fadeTo() { 
    
    }
    /// }
}
