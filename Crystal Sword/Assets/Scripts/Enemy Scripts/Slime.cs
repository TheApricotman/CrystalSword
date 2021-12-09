using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    private Transform target;
    public Transform homePos;
    public float chaseRadius;
    private Animator anim;
    public Collider2D bounds;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckDist();
        GoHome();

    }

    void CheckDist()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        if(Vector3.Distance(target.position, transform.position)<= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, baseSpeed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
        }
    }
    void GoHome()
    {
        Vector3 direction = homePos.position - transform.position;
        direction = direction.normalized;
        if (Vector3.Distance(target.position, transform.position) >= chaseRadius)
        {
            transform.position = Vector3.MoveTowards(transform.position, homePos.position, baseSpeed * Time.deltaTime);
            anim.SetFloat("Horizontal", direction.x);
        }
    }
        
    private bool InHome()
    {
        return (bounds.bounds.Contains(transform.position));
    }
}
