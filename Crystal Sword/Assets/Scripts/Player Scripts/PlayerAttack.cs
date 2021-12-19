using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float atkWait;
    public float startAtkWait;
    private float chargeTime;
    public float startChargeTime;
    public Animator anim;
    public Transform atkPos;
    public float atkRange;
    public float aoeRange;
    public LayerMask whatIsEnemy;
    public int damage;
    public bool charged;
    public KnockBack knockBack;
    public Health health;

    private void Start()
    {
        chargeTime = startChargeTime;
        health = GetComponent<Health>();
    }

    private void Update()
    {
        BasicAtk();
        AOE();
    }

    private void AOE()
    {
        //holding down space starts charging timer
        if (Input.GetKey(KeyCode.Space))
        {
            chargeTime -= Time.deltaTime;
            anim.SetBool("Charging", true);
            if (chargeTime <= 0)
            {
                charged = true;
                anim.SetBool("Ready", true);
                Debug.Log("Charged");
            }
        }
        //if space is not held down for long enough, player returns to idle state
        if (Input.GetKeyUp(KeyCode.Space) && (!charged))
        {
            anim.SetBool("Charging", false);
            charged = false;
            chargeTime = startChargeTime;

        }
        //if space is held down long enough and released, player will perform AOE attack
        if (Input.GetKeyUp(KeyCode.Space) && charged)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, aoeRange, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<EnemyHealth>().isCrystal)
                {
                    health.GetHealth();
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                else
                enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
            }

            anim.SetTrigger("Release");
            anim.SetBool("Ready", false);
            anim.SetBool("Charging", false);
            charged = false;
            chargeTime = startChargeTime;
        }
    }

    private void BasicAtk()
    {
        if (atkWait <= 0)
        //checks timer to see if player can attack
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                anim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
                if (enemiesToDamage != null)
                {
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        if (enemiesToDamage[i].GetComponent<EnemyHealth>().isCrystal)
                        {
                            health.GetHealth();
                            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                        }
                        else
                        {
                            enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
                            Rigidbody2D enemyRB = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                            knockBack.KnockBackGo(enemyRB);
                        }
                    }
                }
                //looks for enemies in range of sword attack and deals damage

                atkWait = startAtkWait;
                //resets timer
            }
        }
        else atkWait -= Time.deltaTime;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(atkPos.position, atkRange);
        //draws a circle where the attack range is
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aoeRange);
    }

}
