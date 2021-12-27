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
    private bool stairing;

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
            Debug.Log("SOOOOO SLOOOOWWW");
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
        if (stairing)
        {
            movement.y += movement.x;
            movement = movement.normalized;
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        //Checking to see if on stairs, if true, player moves diagonally to go up stairs
        if (collision.CompareTag("Stair"))
        {
            stairing = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        //if not on stairs, movement goes back to normal
        if (collision.CompareTag("Stair"))
        {
            stairing = false;
        }
    }
}
