using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable
{//Stores all basic enemy behaviors, chasing, idling etc, individual enemy actions stored in derived class named after enemy
    [SerializeField]
    private string enemyName;
    [SerializeField]
    protected float baseSpeed;
    [SerializeField]
    protected int health;
    [SerializeField]
    protected int baseAtk;
    protected Transform target;
    [SerializeField]
    protected Transform homePos;
    [SerializeField]
    protected float chaseRadius;
    protected Animator anim;
    [SerializeField]
    protected Collider2D bounds;
    protected Rigidbody2D rigid;
    protected Vector3 directionVector;
    protected bool chasing;
    public bool isCrystal;

    public int Health { get; set; }
    public bool IsCrystal { get; set; }

    protected virtual void Start()
    {
        Init();
    }
    protected void Init()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        rigid = GetComponentInChildren<Rigidbody2D>();
        ChangeDirection();
        Health = health;
    }

    protected virtual void Update()
    {
        CheckDist();
        if (InHome() && !chasing)
        {
            Move();
        }
        else
        {
            GoHome();
        }  
    }

    public void Damage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    protected void ChangeDirection()
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

    protected virtual void Move()
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

    protected void OnCollisionEnter2D(Collision2D collision)
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

    protected virtual void ChangeAnim()
    {
        //updates animator to reflect movement
        anim.SetFloat("Horizontal", directionVector.x);
        anim.SetFloat("Vertical", directionVector.y);
    }

    protected virtual void CheckDist()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            rigid.MovePosition(transform.position + direction * baseSpeed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            chasing = true;
        }
    }
    protected void GoHome()
    {
        Vector3 direction = homePos.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) >= chaseRadius)
        {
            rigid.MovePosition(transform.position + direction * baseSpeed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            chasing = false;
        }
    }

    protected bool InHome()
    {
        return bounds.bounds.Contains(transform.position);
    }

}
