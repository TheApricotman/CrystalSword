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
        BasicAttack();
       
    }

    private void FixedUpdate()
    {
        //Checks if sword is out, if not player can move
        if (!sword)
        {
            playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
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
    void BasicAttack()
    {
        //When space is pushed, character stops and swings sword
        if (Input.GetKeyDown(KeyCode.Space))
        {
            sword=true;
            Debug.Log("YAH!");
            anim.SetBool("Attacking", true);

            StartCoroutine(IsAttacking(1));
        }

    }
    IEnumerator IsAttacking(float atkWait)
    {
       
        yield return new WaitForSeconds(atkWait);
    
         anim.SetBool("Attacking", false);
        sword = false;
        
    }

}
