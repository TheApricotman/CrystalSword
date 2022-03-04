using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D playerRb;
    public GameObject trail;
    private RaycastHit2D hit;
    [SerializeField] private LayerMask walls;
    [SerializeField] private LayerMask whatIsEnemy;
    private Vector2 direction;
    private float dashTime;
    public float startDashTime;
    private float dashSpeed;
    public float startDashSpeed;
    public Health health;
    public int damage;
    public KnockBack knockBack;
    public float atkRange;
    public Transform atkPos;

    
    // Update is called once per frame
    void Update()
    {
        Dash();
        
    }

    private void Dash()
    {
        //stops player from spamming dash attack
        if (dashTime <= 0)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                direction.x = anim.GetFloat("Horizontal");
                direction.y = anim.GetFloat("Vertical");
                WallCheck();
                anim.SetTrigger("Dashing");
                playerRb.MovePosition(playerRb.position + direction.normalized * dashSpeed);
                trail.SetActive(true);
                dashTime = startDashTime;
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
                if (enemiesToDamage != null)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].GetComponent<IDamageable>().IsCrystal)
                        {
                            health.GetHealth();
                            enemiesToDamage[i].GetComponent<IDamageable>().Damage(damage);
                        }
                        else
                        {
                            enemiesToDamage[i].GetComponent<IDamageable>().Damage(damage);
                            Rigidbody2D enemyRB = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                            knockBack.KnockBackGo(enemyRB);
                        }                        
                    }
                }
                
            }
        }
        else dashTime -= Time.deltaTime;
        dashSpeed = startDashSpeed;
    }

    private void WallCheck()
    {
        // checks with raycasting if theres a wall, shortens dash
        hit = Physics2D.Raycast(transform.position, direction, 3f, walls);
        if (hit.collider != null)
        {
            dashSpeed = hit.distance;
        }
        else dashSpeed = startDashSpeed;
    }
}


