using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    private Transform target;
    public Transform homePos;
    [SerializeField]
    private float chaseRadius;
    [SerializeField]
    private float atkRadius;
    [SerializeField]
    private float deathCircleAngle;
    [SerializeField]
    private float multi;
    private Animator anim;
    [SerializeField]
    private Collider2D bounds;
    private Rigidbody2D rigid;
    private Vector3 directionVector;
    private bool chasing;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDist();
        GoHome();
        SpiralAtk();
        if (InHome() && !chasing)
        {
            Move();

        }
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

    private void Move()
    {
        Vector3 temp = transform.position + directionVector * baseSpeed * Time.deltaTime;
        if (bounds.bounds.Contains(temp))
        {
            rigid.MovePosition(temp);
        }
        else if (!bounds.bounds.Contains(temp))
        {
            ChangeDirection();
        }
    }

    /* private void OnCollisionEnter2D(Collision2D collision)
     {

         Vector3 temp = directionVector;
         int loops = 0;
         ChangeDirection();
         while (temp == directionVector && loops < 100)
         {
             loops++;
             ChangeDirection();
         }

     }*/
    void ChangeAnim()
    {
        directionVector = directionVector.normalized;
        //updates animator to reflect movement
        anim.SetFloat("Vertical", directionVector.x);

    }


    void CheckDist()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            rigid.MovePosition(transform.position + direction * baseSpeed * Time.deltaTime);
            anim.SetFloat("Vertical", direction.x);
            anim.SetBool("Chasing", true);
            chasing = true;
        }

    }

    void SpiralAtk()
    {
      
        if (Vector3.Distance(target.position, transform.position) <= atkRadius)
        {
            Vector3 direction = target.position - transform.position;
            direction = Quaternion.Euler(0, 0, deathCircleAngle) * direction;
            float distanceThisFrame = (baseSpeed*multi) * Time.deltaTime;

            rigid.MovePosition(transform.position + direction.normalized * distanceThisFrame);

        }
    }

    void GoHome()
    {
        Vector3 direction = homePos.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) >= chaseRadius)
        {
            rigid.MovePosition(transform.position + direction * baseSpeed * Time.deltaTime);
            anim.SetFloat("Vertical", direction.x);
            anim.SetBool("Chasing", false);
            chasing = false;
        }
    }

    private bool InHome()
    {
        return (bounds.bounds.Contains(transform.position));
    }
}

