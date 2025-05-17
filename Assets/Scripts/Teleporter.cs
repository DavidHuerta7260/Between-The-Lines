using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{

    public PlayerController player;
    public Transform teleportTarget;

    public GameObject hiderPrefab;

    public bool spokToSusp = false;

    private bool playerInRange = false;

    public NPCDialogue npcDialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange)
        {

            if (spokToSusp == true)
            {
                if (Input.GetKeyDown(KeyCode.Y))
                {
                    npcDialogue.EndDialog();
                    fadeTo();
                    teleportPlayer();

                }


            }

        }
    }

    public void fadeTo()
    {
        Instantiate(hiderPrefab, player.transform.position, Quaternion.identity);
    }

    public void teleportPlayer()
    {
        player.enabled = false;
        player.transform.position = teleportTarget.position;
        player.enabled = true;

    }

    public bool CanTelo() {
        return spokToSusp = true;
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
            //EndDialog();
        }
    }
}
