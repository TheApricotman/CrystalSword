using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    private LaserEmitter2 lEmitter;
    [SerializeField]
    private StartPuzzle startPuzzle;

    [SerializeField]
    private Vector2 playerPos;
    [SerializeField]
    private VectorValue playerStorage;
    [SerializeField]
    private Animator transition;

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
                playerStorage.intialValue = playerPos;
                transition.SetTrigger("StartFade");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            yield break;
        }
        else
        {
            yield break;
        }
    }
        
}
