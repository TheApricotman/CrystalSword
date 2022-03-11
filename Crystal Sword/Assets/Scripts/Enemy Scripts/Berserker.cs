using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserker : Enemy
{
    //Berserker idles until player in range, goes to attack mode and flies straight at player, requiring player to dodge.
    //if berserker hits wall in attack mode, goes to recovery mode where it stays until hit or after certain amount of time
    [SerializeField]
    private float atkRadius;
    [SerializeField]
    private float knockTimer;
    private float speedReset;
    [SerializeField]
    private GameObject trail;


    // Start is called before the first frame update
    protected override void Start()
    {
        Init();
        speedReset = baseSpeed;
    }

    // Update is called once per frame
   protected override void Update()
    {
        CheckDist();
    }

    IEnumerator KnockedCount(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        anim.SetBool("KnockedOut", false);
        anim.SetBool("AttackMode", false);
    }

    protected override void CheckDist()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius)
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
        }
        if (Vector3.Distance(target.position, transform.position) <= atkRadius)
        {
            anim.SetBool("AttackMode", true);
        }
        else anim.SetBool("AttackMode", false);
    }

    private void Charge()
    {
        WallCheck();
        trail.SetActive(true);
        Vector3 atkDirection = target.position - transform.position;
        atkDirection = atkDirection.normalized;
        transform.Translate(atkDirection * baseSpeed);
        //rigid.MovePosition(transform.position + atkDirection * baseSpeed);
        baseSpeed = speedReset;
    }

    protected override void WallCheck()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        // checks with raycasting if theres a wall, 
        hit = Physics2D.Raycast(transform.position, direction, baseSpeed, walls);
        if (hit.collider != null)
        {
            baseSpeed = hit.distance;
            anim.SetBool("KnockedOut", true);
            StartCoroutine(KnockedCount(knockTimer));
        }
    }
}
