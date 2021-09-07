using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Rigidbody2D playerRb;
    Vector2 movement;
    public Animator anim;
    private bool sword;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void FixedUpdate()
    {
        //moves character with physics
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Move()
    {
        //Player can move and gives animator correct direction of Player
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            anim.SetFloat("Horizontal", movement.x);
            anim.SetFloat("Vertical", movement.y);
        }

        anim.SetFloat("Speed", movement.sqrMagnitude);
    }

}
