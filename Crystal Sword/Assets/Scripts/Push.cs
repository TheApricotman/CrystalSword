using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public Animator playerAnim;

    private bool touching;
    private bool pushing;
    private bool returning;
    private bool moving;
    private Vector3 oGPos;
    private Vector2 destination;
    private Vector3 lastPos;
    private RaycastHit2D hit;
    public LayerMask walls;

    private void Start()
    {
        oGPos = transform.position;
    }

    private void Update()
    {
        PushCalc();
        ReturnCalc();
    }

    private void PushCalc()
    {
        //allows player to push block around, sliding gently according to grid
        //checks if player is in contact with block and is not moving
        if (touching && !moving)
        {
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                pushing = true;
                //Finds direction of character and gives correct coordinates to push block
                destination.x = transform.position.x + playerAnim.GetFloat("Horizontal");
                destination.y = transform.position.y + playerAnim.GetFloat("Vertical");
               
            }
        }
        if (pushing)
        {
            transform.position = Vector2.MoveTowards(transform.position, destination, 1 * Time.deltaTime);
            moving = true;

            if ((Vector2)transform.position == destination)
            {
                pushing = false;
                transform.position = destination;
                moving = false;
            }
        }
    }
    private void ReturnCalc()
    {
        //Logic for returning puzzle blocks to original position for restart, functionally the same as PushCalc()
        //using input for control for now, but will be controlled with sword in stone
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            returning = true;
        }

        if (returning)
        {
            transform.position = Vector2.MoveTowards(transform.position, oGPos, 1 * Time.deltaTime);

            if (transform.position == oGPos)
            {
                returning = false;
                transform.position = oGPos;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //If player is touching block, makes bool true;
        if (other.gameObject.CompareTag("Player"))
        {
            touching = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        //when player stops touching block, sets bool to false
        if (other.gameObject.CompareTag("Player"))
        {
            touching = false;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, destination);
    }

}
