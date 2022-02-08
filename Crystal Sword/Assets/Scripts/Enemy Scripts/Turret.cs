using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField]
    private float range;
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private float shootTime;
    private float shootTimer;


    // Update is called once per frame
    protected override void Update()
    {
        if (shootTimer <= 0)
        {
            if (InRange())
            {
                shootTimer = shootTime;
                anim.SetTrigger("Shooting");
            }
        }
        else shootTimer -= Time.deltaTime;
    }

    void Shoot()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        GameObject current = Instantiate(projectile, shootPoint.position, Quaternion.identity);
        current.GetComponent<Projectile>().Launch(direction);
    }

    private bool InRange()
    {
        return Vector3.Distance(target.position, transform.position) <= range;
    }
}
