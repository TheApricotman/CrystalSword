using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallinHoles : MonoBehaviour
{
    [SerializeField]
    private Animator playerAnim;
    private PlayerMovement move;
    private Rigidbody2D playerRB;
    public VectorValue startPos;
    public Health health;
    private Vector3 offset = new Vector3(0, .5f, 0);

    private void Start()
    {
        move = GetComponent<PlayerMovement>();
        playerRB = GetComponent<Rigidbody2D>();
    }
    //Simple falling mechanic, if player hits hole, plays the fall animation and resets position to start area
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            move.enabled = false;
            playerRB.velocity = Vector3.zero;
            playerAnim.SetTrigger("Fall");
            transform.position = collision.transform.position - offset;
            health.TakeDamage(1);
            StartCoroutine(FallWait());          
        }    
    }
    IEnumerator FallWait()
    {
        yield return new WaitForSeconds(1);
        transform.position = startPos.intialValue;
        playerAnim.Play("Idle");
        move.enabled = true;
    }
}
