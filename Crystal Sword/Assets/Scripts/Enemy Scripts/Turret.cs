using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : Enemy
{
    [SerializeField]
    private float range;
    [SerializeField]
    public GameObject projectile;
    public Transform player;
    [SerializeField]
    private float shootTime;
    private float shootTimer;


    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (shootTimer <= 0)
        {            
            if (InRange())
            {
                shootTimer = shootTime;
                Vector3 direction = player.position - transform.position;
                GameObject current = Instantiate(projectile, transform.position, Quaternion.identity);
                current.GetComponent<Projectile>().Launch(direction);
                
            }
        }
        else shootTimer -= Time.deltaTime;
    }

    private bool InRange()
    {
        return Vector3.Distance(player.position, transform.position) <= range;
    }
}
