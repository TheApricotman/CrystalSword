﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    private float slowSpeed = 2f;
    public Rigidbody2D playerRb;
    Vector2 movement;
    public Animator anim;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        //checks to see if player is attacking or AOEing,if true, player cannot move, if dashing, disables moveposition
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack") && !anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Release")
            && !anim.GetCurrentAnimatorStateInfo(0).IsName("Dash"))
        {
            //moves character with physics
            playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
        //if charging AOE attack, limit movement
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Charge Walk") || anim.GetCurrentAnimatorStateInfo(0).IsName("AOE Ready Walk"))
        {
            playerRb.MovePosition(playerRb.position + movement * slowSpeed * Time.fixedDeltaTime);
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
