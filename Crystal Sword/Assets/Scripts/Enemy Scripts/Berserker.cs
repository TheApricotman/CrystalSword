﻿using System.Collections;
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
    private bool charging =false;
    private Vector3 direction;


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

    private void FixedUpdate()
    {  
        if (charging)
        {
            StartCoroutine(ChargeDelay());
            rigid.MovePosition(transform.position + direction * baseSpeed);
            
            charging = false;
            baseSpeed = speedReset;
        }
    }
    IEnumerator ChargeDelay()
    {
        direction = target.position - transform.position;
        direction = direction.normalized;
        yield return new WaitForSeconds(1f);      
    }

    private void Charge()
    {
        trail.SetActive(true);
        Vector3 atkDirection = target.position - transform.position;
        atkDirection = atkDirection.normalized;
        WallCheck(atkDirection);
        charging = true;
    }

    protected override void WallCheck(Vector3 direction)
    {
        // checks with raycasting if theres a wall, 
        hit = Physics2D.Raycast(transform.position, direction, baseSpeed);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Wall"))
            {
                baseSpeed = hit.distance;
                anim.SetBool("KnockedOut", true);
                StartCoroutine(KnockedCount(knockTimer));
            }
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                baseSpeed = hit.distance;
            }
        }
    }

    new private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRB = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRB.velocity = Vector2.zero;
            anim.SetBool("KnockedOut", true);
            StartCoroutine(KnockedCount(1));
        }
    }
}
