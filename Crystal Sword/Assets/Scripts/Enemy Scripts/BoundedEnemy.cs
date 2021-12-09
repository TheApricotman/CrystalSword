using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedEnemy : MonoBehaviour
{
    private Vector3 directionVector;
    private Transform myTransform;
    public Transform player;
    public float speed;
    public float agroRange;
    private Rigidbody2D rB2D;
    private Animator anim;
    public Collider2D bounds;
    private bool playerInRange;
    private bool chasing;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        rB2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        ChangeDirection();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!chasing)
        { Move(); }

            SightPlayer();
    }
    

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * Time.deltaTime * speed;
        if (bounds.bounds.Contains(temp))
        {
            rB2D.MovePosition(temp);
        }
        else if (!bounds.bounds.Contains(temp))
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        //Switch statement to choose a direction to move in when patrolling
        int direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0:
                //walking to the right
                directionVector = Vector3.right;
                break;
            case 1:
                //walking down
                directionVector = Vector3.down;
                break;
            case 2:
                //walking up
                directionVector = Vector3.up;
                break;
            case 3:
                //walking left
                directionVector = Vector3.left;
                break;
            default:
                break;

        }
        ChangeAnim();
    }

    void ChangeAnim()
    {
        //updates animator to reflect movement
        anim.SetFloat("Horizontal", directionVector.x);
        anim.SetFloat("Vertical", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Vector3 temp = directionVector;
        int loops = 0;
        ChangeDirection();
        while (temp == directionVector && loops < 100)
        {
            loops++;
            ChangeDirection();
        }

    }

    private void SightPlayer()
    {
        Vector3 currentPos = transform.position;
        Vector3 chaseDirection = player.position - currentPos;

        if (Vector2.Distance(transform.position, player.position) < agroRange)
        {
            chasing = true;
            rB2D.MovePosition(currentPos + chaseDirection * Time.deltaTime * speed);
            ChangeAnim();
        }
        else
        {
            Vector3 homeDirection = bounds.transform.position - currentPos;
            chasing = false;
            
            if (!bounds.bounds.Contains(currentPos))
            {
                rB2D.MovePosition(currentPos + homeDirection * Time.deltaTime * speed);
                ChangeAnim();
            }
        }
    }

}
