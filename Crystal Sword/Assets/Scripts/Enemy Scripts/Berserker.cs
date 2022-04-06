using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : Enemy
{
    //Berserker idles until player in range, goes to attack mode and flies straight at player, requiring player to dodge.
    //if berserker hits wall in attack mode, goes to recovery mode where it stays until hit or after certain amount of time
    [SerializeField]
    private float rushDist;
    [SerializeField]
    private float atkRadius;
    [SerializeField]
    private float knockTimer;
    private float distReset;
    [SerializeField]
    private GameObject trail;
    private Vector3 direction;


    // Start is called before the first frame update
    protected override void Start()
    {
        Init();
        distReset = rushDist;
    }

    // Update is called once per frame
   protected override void Update()
    {
        CheckDist();
        GoHome();
    }

    IEnumerator KnockedCount(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetBool("KnockedOut", false);
        anim.SetBool("AttackMode", false);
    }

    protected override void CheckDist()
    {
        Vector3 lookDirection = target.position - transform.position;
        lookDirection = lookDirection.normalized;
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            anim.SetFloat("Horizontal", lookDirection.x);
            anim.SetFloat("Vertical", lookDirection.y);
        }
        if (Vector3.Distance(target.position, transform.position) <= atkRadius)
        {
            anim.SetBool("AttackMode", true);
        }
        else anim.SetBool("AttackMode", false);
    }

    private void AimDelay()
    {
        //triggered through animation events, gives the direction of attack a fraction of a second before firing
        direction = target.position - transform.position;
        direction = direction.normalized;              
    }

    private void Charge()
    {
        //triggered through animation events, checks for obstacles and charges toward desired direction
        trail.SetActive(true);
        Vector3 atkDirection = target.position - transform.position;
        atkDirection = atkDirection.normalized;
        WallCheck(atkDirection);        
        rigid.MovePosition(transform.position + direction * rushDist);
        rushDist = distReset;
    }

    protected override void WallCheck(Vector3 direction)
    {
        // checks with raycasting if theres an obstacle, and updates the base speed accordingly 
        hit = Physics2D.Raycast(transform.position, direction, rushDist);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                rushDist = hit.distance;
                anim.SetBool("KnockedOut", true);
                StartCoroutine(KnockedCount(knockTimer));
            }
            if (hit.collider.gameObject.CompareTag("Player"))
            { 
                anim.SetBool("KnockedOut", true);
                StartCoroutine(KnockedCount(1));
            }
        }
    }

    new private void OnCollisionEnter2D(Collision2D collision)
    {//bandaid to stop player character from flying when hit 
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRB.velocity = Vector2.zero;
   
        }
    }
}
