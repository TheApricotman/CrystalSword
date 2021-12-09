using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numOfBlocks;
    public Image[] healthBlocks;
    public Sprite fullBlock;
    public Sprite emptyBlock;
    private Rigidbody2D playerRB;
    private PlayerMovement move;


    private void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        
        //stops player from collecting more health if already full
        if (health > numOfBlocks)
        {
            health = numOfBlocks;
        }

        //Health bar image logic
        for (int i = 0; i < healthBlocks.Length; i++)
        {
            if (i < health)
            {
                healthBlocks[i].sprite = fullBlock; 
            }
            else
            {
                healthBlocks[i].sprite = emptyBlock;
            }
            if (i < numOfBlocks)
            {
                healthBlocks[i].enabled = true;
            }
            else
             { 
                healthBlocks[i].enabled = false;
            }
        }
    }
    public void TakeDamage(int damage)
    { 
            health -= damage;
            Debug.Log("Ouch!");    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            move.enabled = false;
            float thrust = 10;
            TakeDamage(1);
            Vector3 difference = collision.transform.position - transform.position;
            difference = difference.normalized * thrust;
            playerRB.AddForce(difference , ForceMode2D.Impulse);
            StartCoroutine(KnockCo(playerRB, 0.3f));
        }

    }
    private IEnumerator KnockCo(Rigidbody2D player, float knockTime)
    {
        if (player != null)
        {
            yield return new WaitForSeconds(knockTime);
            player.velocity = Vector2.zero;
            move.enabled = true;
        }
    }
}
