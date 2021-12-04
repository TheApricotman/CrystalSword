using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundedNPC : MonoBehaviour
{
    private Vector3 directionVector;
    private Transform myTransform;
    public float speed;
    private Rigidbody2D rB2D;
    private Animator nPCAnim;
    public Collider2D bounds;
    private bool playerInRange;
    private float prevSpeed;

    // Start is called before the first frame update
    void Start()
    {
        myTransform = GetComponent<Transform>();
        rB2D = GetComponent<Rigidbody2D>();
        nPCAnim = GetComponent<Animator>();
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerInRange)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 temp = myTransform.position + directionVector * speed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            rB2D.MovePosition(temp);
        }
        else ChangeDirection();
    }

    void ChangeDirection()
    {
        //Switch statement to choose a direction to move in
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
        //updates animator to reflet movement
        nPCAnim.SetFloat("Horizontal", directionVector.x);
        nPCAnim.SetFloat("Vertical", directionVector.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!collision.gameObject.CompareTag("Player"))
        //{
            //Character changes direction if colliding with something
            Vector3 temp = directionVector;
            int loops = 0;
            ChangeDirection();
            while (temp == directionVector && loops < 100)
            {
                loops++;
                ChangeDirection();
            }
       // }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {         
            playerInRange = true;
            prevSpeed = nPCAnim.speed;
            nPCAnim.speed = 0;
            if (collision.transform.position.x < transform.position.x)
            {
                directionVector = Vector3.down;
            }
            else directionVector = Vector3.up;
            if (collision.transform.position.y < transform.position.y)
            {
                directionVector = Vector3.left;
            }
            else directionVector = Vector3.right;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            nPCAnim.speed = prevSpeed;
            playerInRange = false;
        }
    }
}
