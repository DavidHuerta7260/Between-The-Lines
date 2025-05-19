using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Prompter : MonoBehaviour
{
    public PlayerController player;
    public Transform teleportTarget;
    public Transform teleportTarget2;

    public GameObject prompterBox;
    public TextMeshProUGUI prompterContent;
    public string[] prompterLines;

    private bool prompterActive = false;
    public bool hasPrompted = false;

    public Teleporter teleporter;
    public GameObject hider;

    public NPCDialogue npcDialogue;

    public Transform DecisionSpawn1prefab;
    public Transform DecisionSpawn2prefab;
    public GameObject DecisionArea1;
    public GameObject DecisionArea2;

    public int jEntryCount = 0;

    static int factor = -1;

    private bool decisionAreaSpawned = false;
    public float delay = 30f;

    public JournalManager journalManager;

    void Update()
    {
        if (prompterActive)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                teleportPlayer();
                EndPrompter();
            }

            if (Input.GetKeyDown(KeyCode.N))
            {
                prompterContent.text = "When you're ready go back to the center of town and press Y again.";
                EndPrompter();

                if (!decisionAreaSpawned)
                {
                    DecisionArea1.transform.position = DecisionSpawn1prefab.position;
                    DecisionArea2.transform.position = DecisionSpawn2prefab.position;
                    decisionAreaSpawned = true;
                }
            }
        }

        if (factor == 10 && !hasPrompted)
        {
            hasPrompted = true;
            StartCoroutine(PromptAfterDelay());
        }
    }

    public void StartPrompter()
    {
        prompterBox.SetActive(true);
        prompterContent.text = "It seems we have enough evidence to proceed with the Trial? \nYes or No \n(On the Keyboard Hit Y for yes and N for no)";
        prompterActive = true;
    }

    void EndPrompter()
    {
        prompterActive = false;
        StartCoroutine(HidePrompterAfterDelay(2f));
    }

    public void jounalInc()
    {
        factor++;

        if (jEntryCount == 5 && !hasPrompted)
        {
            StartPrompter();
            hasPrompted = true;

            if (journalManager != null && journalManager.journalButton != null)
            {
                journalManager.journalButton.SetActive(true);
            }
        }
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

    public void teleportPlayer()
    {
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

    public void fadeTo()
    {
        // Placeholder for fade effect logic
    }
}
