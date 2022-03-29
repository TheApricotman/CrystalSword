using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public GameObject dialogBox;
	public Dialog dialog;
    
    private bool playerInRange;
    public DialogManager dialogManager;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInRange && !dialogBox.activeInHierarchy)
        {
            TriggerDialogue();
        }
        if (dialogBox.activeInHierarchy && Input.GetKeyDown(KeyCode.R) && playerInRange)
        {
            dialogManager.DisplayNextSentence();
        }
    }

    public void TriggerDialogue()
	{
		FindObjectOfType<DialogManager>().StartDialog(dialog);
        dialogBox.SetActive(true);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;           
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
        }
    }

}
