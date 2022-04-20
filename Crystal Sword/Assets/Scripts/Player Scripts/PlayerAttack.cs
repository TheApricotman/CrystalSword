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
    [SerializeField]
    private GameObject aoeAnim;
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
            StartCoroutine(LingerHit());
            aoeAnim.SetActive(true);          
            StartCoroutine(AnimWait(1f));
          
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

    IEnumerator LingerHit()
    {
        int loops= 0;
        while (loops < 100 )
        {
            loops++;
            Debug.Log("Im fuckign coroutiningbruh");
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(transform.position, aoeRange, whatIsEnemy);

            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if (enemiesToDamage[i].GetComponent<IDamageable>().IsCrystal)
                {
                    health.GetHealth();
                    enemiesToDamage[i].GetComponent<IDamageable>().Damage(damage);
                    Debug.Log("Hitting" + enemiesToDamage);
                   
                }
                else
                {
                    enemiesToDamage[i].GetComponent<IDamageable>().Damage(damage);
                    Rigidbody2D enemyRB = enemiesToDamage[i].GetComponent<Rigidbody2D>();
                    knockBack.KnockBackGo(enemyRB);
                    Debug.Log("Hitting" + enemiesToDamage);
                 
                }
            }
            
        }
        yield return null;        
    }

    IEnumerator AnimWait(float animLength)
    {
        yield return new WaitForSeconds(animLength);
        aoeAnim.SetActive(false);
    }

}
