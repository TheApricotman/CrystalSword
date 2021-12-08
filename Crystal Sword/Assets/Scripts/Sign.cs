using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour
{
    public GameObject dialogBox;
    public Text dialogText;
    public Dialog talkDialog;
    [TextArea (3,10)]
    public string dialog;
    public bool playerInRange;
    public bool person;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && playerInRange && !person)
        {
            if (dialogBox.activeInHierarchy)
            {
                dialogBox.SetActive(false);
            }
            else
            {
                dialogBox.SetActive(true);
                    dialogText.text = dialog;
            }
        } 

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("player entered range");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
            Debug.Log("Player left range");
        }
    }
}
