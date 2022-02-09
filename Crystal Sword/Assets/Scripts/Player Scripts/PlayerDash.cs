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
        //pulls players direction data
        direction.x = anim.GetFloat("Horizontal");
        direction.y = anim.GetFloat("Vertical");
    }

    private void FixedUpdate()
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
                WallCheck();
                anim.SetTrigger("Dashing");
                playerRb.MovePosition(playerRb.position + direction * dashSpeed);
                trail.SetActive(true);

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
                if (enemiesToDamage != null)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].GetComponent<Enemy>().isCrystal)
                        {
                            health.GetHealth();
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                        }
                        else
                        {
                            enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                            Rigidbody2D enemyRB = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                            knockBack.KnockBackGo(enemyRB);
                        }
                        dashTime = startDashTime;
                    }
                }
                else dashTime -= Time.deltaTime;
                dashSpeed = startDashSpeed;
            }
        }
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


