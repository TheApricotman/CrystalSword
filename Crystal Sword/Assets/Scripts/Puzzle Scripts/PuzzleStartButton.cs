using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleStartButton : MonoBehaviour
{
    [SerializeField]
    private Rotate rotate;
    [SerializeField]
    private Push push;
    [SerializeField]
    private FireLaser laser;
    [SerializeField]
    private Animator playerAnim;
    public bool startPuzzle;
    public bool winPuzzle;

    private void Start()
    {
        startPuzzle = false;
        winPuzzle = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Button") )//&& Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Pushing Button");
            playerAnim.SetTrigger("Take Sword");
            startPuzzle = true;
            if (laser.win)
            {
                Debug.Log("Win");
            }
            if (laser.lose)
            {
                Debug.Log("Lose!");
                push.ReturnCalc();
                rotate.ReturnCalc();
            }
        }
    }
}
