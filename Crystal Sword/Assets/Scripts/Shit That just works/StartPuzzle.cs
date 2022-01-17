using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPuzzle : MonoBehaviour
{
    private bool ready;
    public bool go;


    private void Awake()
    {
        ready = false;
        go = false;
    }

    private void Update()
    {
        if(ready && Input.GetKeyDown(KeyCode.R))
        {
            go = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ready = true;
        }        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ready = false;
        }
    }

    private void Reset()
    {
        ready = false;
        go = false;
    }

    private void OnEnable()
    {
        PuzzleManager.Resetting += Reset;
    }

    private void OnDisable()
    {
        PuzzleManager.Resetting -= Reset;
    }
}
