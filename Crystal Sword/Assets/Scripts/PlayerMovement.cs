using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float slowSpeed = 2f;
    public Rigidbody2D playerRb;
    Vector2 movement;
    public Animator anim;
    public VectorValue startPos;

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
        //checks to see if player is attacking, AOEing or shooting,if true, player cannot move, if dashing, disables normal movement
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Release")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash") && !anim.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            //moves character with physics
            //playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
            playerRb.velocity = movement * moveSpeed;
        }
        else playerRb.velocity = Vector2.zero;
        //if charging AOE attack, limit movement
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Charge Walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Ready Walk"))
        {
            //playerRb.MovePosition(playerRb.position + movement * slowSpeed * Time.fixedDeltaTime);
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

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

}
