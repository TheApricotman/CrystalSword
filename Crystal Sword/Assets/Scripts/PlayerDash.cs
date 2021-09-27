using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D playerRb;
    private Vector2 direction;
    private float dashTime;
    public float startDashTime;
    private float dashSpeed;
    public float startDashSpeed;


    // Update is called once per frame
    void Update()
    {
        //pulls players direction data
        direction.x = anim.GetFloat("Horizontal");
        direction.y = anim.GetFloat("Vertical");
    }
    private void FixedUpdate()
    {
        Dash();
    }
    private void Dash()
    {
        //stops player from spamming dash attack
        if (dashTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                float dashMulti = 5f;
                anim.SetTrigger("Dashing");
                //playerRb.MovePosition(playerRb.position + direction * dashSpeed * Time.fixedDeltaTime);
                playerRb.velocity = direction * dashSpeed;
                dashSpeed -= dashSpeed * dashMulti * Time.deltaTime;
                
                dashTime = startDashTime;
                Debug.Log("WOOO!");
            }
        }
        else dashTime -= Time.deltaTime;
        dashSpeed = startDashSpeed;

    }
}
