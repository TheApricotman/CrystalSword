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
    [SerializeField]
    protected Transform homePos;
    [SerializeField]
    protected float chaseRadius;
    [SerializeField]
    protected Collider2D bounds;
    protected Transform target;
    protected Animator anim;
    protected Rigidbody2D rigid;
    protected RaycastHit2D hit;
    [SerializeField]
    protected LayerMask walls;
    protected Vector3 directionVector;
    protected bool chasing;

    public bool isKnockable;
    //damage flicker stuff
    protected SpriteRenderer sprite;
    private int flicker = 5;
    private float flickerTime = 0.1f;
    private Color ogColour;


    //idamageable Stuff
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
        sprite = GetComponentInChildren<SpriteRenderer>();
        ChangeDirection();
        Health = health;
        ogColour = sprite.color;
    }

    protected virtual void Update()
    {
        CheckDist();
        if (InHome() && !chasing)
        {
            Move();
        }
        else if (!InHome())
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
        else StartCoroutine(DamageFlash());
    }
    IEnumerator DamageFlash()
    {
        for (int i = 0; i < flicker; i++)
        {
            sprite.color = new Color(1f, 1f, 1f, .5f);
            yield return new WaitForSeconds(flickerTime);
            sprite.color = ogColour;
            yield return new WaitForSeconds(flickerTime);
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
            WallCheck(directionVector);
            transform.Translate(directionVector * baseSpeed * Time.deltaTime);   
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
            transform.position = Vector3.MoveTowards(transform.position, target.position, baseSpeed * Time.deltaTime);
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
            transform.position = Vector3.MoveTowards(transform.position, homePos.position, baseSpeed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            chasing = false;
        }
    }
    protected virtual void WallCheck(Vector3 direction)
    {
        direction = direction.normalized;
        // checks with raycasting if theres a wall, 
        hit = Physics2D.Raycast(transform.position, direction, 1, walls);
        if (hit.collider != null)
        {
            ChangeDirection();
        }
    }

    protected bool InHome()
    {
        return bounds.bounds.Contains(transform.position);
    }

}
