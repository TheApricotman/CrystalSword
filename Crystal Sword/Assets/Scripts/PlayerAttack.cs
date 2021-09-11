using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float atkWait;
    public float startAtkWait;

    public Animator anim;

    public Transform atkPos;
    public float atkRange;
    public LayerMask whatIsEnemy;
    public int damage;
    


    private void Update()
    {
        BasicAtk();
        
    }

    private void BasicAtk()
    {
        if (atkWait <= 0)
            //checks timer to see if player can attack
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Pressed!");
                anim.SetTrigger("Attack");
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(atkPos.position, atkRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    enemiesToDamage[i].GetComponent<EnemyHealth>().TakeDamage(damage);
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
    }

}
