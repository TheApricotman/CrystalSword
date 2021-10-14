using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D playerRb;
    public GameObject projectile;
    private Vector2 direction;
    public Transform atkPos;
    private float shootTime;
    public float startShootTime;
    

    // Update is called once per frame
    void Update()
    {
        //pulls players direction data
        direction.x = anim.GetFloat("Horizontal");
        direction.y = anim.GetFloat("Vertical");

        Shoot();
    }
    private void Shoot()
    {
        //stops player from spamming Shoot with timer
        if (shootTime <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                
                anim.SetTrigger("Shooting");
                shootTime = startShootTime;
                Debug.Log("POW!");
                Instantiate(projectile, atkPos.position, Quaternion.identity);
            }
        }
        else shootTime -= Time.deltaTime;
    }

}
