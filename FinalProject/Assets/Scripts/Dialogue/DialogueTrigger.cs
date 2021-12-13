using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
   /* [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;*/

    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;
    [SerializeField] private int soundIndex;

    private bool playerInRange;
    private void Awake()
    {
        playerInRange = false;
        //visualCue.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && !DialogueManager.GetInstance().dialogueIsPlaying)
        {
            //visualCue.SetActive(true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                DialogueManager.GetInstance().EnterDialogueMode(inkJSON);
                FindObjectOfType<HandleSound>().PlaySound(soundIndex);
            }
        }
        else
        {
            //wvisualCue.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        playerInRange = false;
    }
}
