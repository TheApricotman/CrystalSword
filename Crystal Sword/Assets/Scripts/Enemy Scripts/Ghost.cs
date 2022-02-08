using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    [SerializeField]
    private float atkRadius;
    [SerializeField]
    private float deathCircleAngle;
    [SerializeField]
    private float multi;
  
    // Update is called once per frame
    protected override void Update()
    {
        CheckDist();
        GoHome();
        SpiralAtk();
        if (InHome() && !chasing)
        {
            Move();
        }
    }

    void SpiralAtk()
    {
      
        if (Vector3.Distance(target.position, transform.position) <= atkRadius)
        {

            anim.SetBool("Chasing", true);
            Vector3 direction = target.position - transform.position;
            direction = Quaternion.Euler(0, 0, deathCircleAngle) * direction;
            float distanceThisFrame = (baseSpeed*multi) * Time.deltaTime;

            rigid.MovePosition(transform.position + direction.normalized * distanceThisFrame);

        }else { anim.SetBool("Chasing", false); }
    }
}

