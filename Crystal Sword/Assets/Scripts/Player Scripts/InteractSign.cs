using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractSign : MonoBehaviour
{
    [SerializeField]
    private GameObject interact;
    private bool interacting;

    //Logic for interact sign above players head to appear when infront of interactable
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && interacting)
        {
            interact.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interact.SetActive(true);
            interacting = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Interactable"))
        {
            interact.SetActive(false);
            interacting = false;
        }
    }
}
