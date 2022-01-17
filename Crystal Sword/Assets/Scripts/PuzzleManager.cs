using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private LaserEmitter2 lEmitter;
    [SerializeField]
    private StartPuzzle startPuzzle;

    public delegate void ResetPuzzle();
    public static event ResetPuzzle Resetting;

    // Update is called once per frame
    void Update()
    {
        if (startPuzzle.go)
        {
            lEmitter.StartPuzzle();
            StopCoroutine(PuzzleWait(2));
            StartCoroutine(PuzzleWait(2));
        }

    }
    

    IEnumerator PuzzleWait(float seconds)
{
    yield return new WaitForSeconds(seconds);

        if (!lEmitter.win)
        {
            if (Resetting != null)
            {
                Resetting();
                
            }

            Debug.Log("Holy Shit This Hot Trash Returned!");
        }

        else Debug.Log("You Win");
    }
        
}
