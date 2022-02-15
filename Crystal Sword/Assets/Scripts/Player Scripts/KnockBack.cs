using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;

    public void KnockBackGo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            Enemy enemyMovement = enemy.GetComponent<Enemy>();
            if (enemyMovement.isKnockable)
            {
                enemyMovement.enabled = false;
                enemy.isKinematic = false;
                Vector3 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);               
                StartCoroutine(EnemyKnockCo(enemy));
                enemyMovement.enabled = true;
            }
        }
    }

    private IEnumerator EnemyKnockCo(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(knockTime);
            enemy.velocity = Vector2.zero;
            enemy.isKinematic = true;
            Debug.Log("I Knocked back a " + enemy.name);
        } 
    }
}
