using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float slowSpeed = 2f;
    [SerializeField]
    private Rigidbody2D playerRb;
    public Vector2 movement;
    public Animator anim;
    public VectorValue startPos;
    private bool stairLeft;
    private bool stairRight;

    private void Start()
    {
        transform.position = startPos.intialValue;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        //checks to see if player is attacking, AOEing or shooting,if true, player cannot move
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Release")
             && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot") && !anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerFall"))
        {
            //moves character with physics
            playerRb.velocity = movement * moveSpeed;
        }
        else playerRb.velocity = Vector2.zero;
        //if charging AOE attack, limit movement
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Charge Walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Ready Walk"))
        {
            playerRb.velocity = movement * slowSpeed; 
        }
       

    }

    void Move()
    {
        //Player can move and gives animator correct direction of Player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        if (movement != Vector2.zero)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }
        if (stairRight)
        {        
                movement.y += movement.x;
                movement = movement.normalized;            
        }

        if (stairLeft)
        {
            movement.y -= movement.x;
            movement = movement.normalized;
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Checking to see if on stairs, if true, player moves diagonally to go up stairs
        if (collision.CompareTag("StairRight"))
        {
            stairRight = true;
        }
        if (collision.CompareTag("StairLeft"))
        {
            stairLeft = true;
        }

        //if near a ladder, snaps to center of ladder and only allows up and down movement
        if (collision.CompareTag("Ladder"))
        {
            float ladderCenter = collision.transform.position.x;

            transform.position = new Vector3(ladderCenter, transform.position.y, 0);
            movement.x = 0;
            anim.SetBool("Climbing", true);
            if (movement == Vector2.zero)
            {
                anim.speed = 0;
            }
            else anim.speed = 1;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            movement.y = Input.GetAxisRaw("Vertical");
            anim.SetBool("Climbing", false);
            anim.speed = 1;
        }

        //if not on stairs, movement goes back to normal
        if (collision.CompareTag("StairRight"))
        {
            stairRight = false;
        }
        if (collision.CompareTag("StairLeft"))
        {
            stairLeft = false;
        }
    }
}
