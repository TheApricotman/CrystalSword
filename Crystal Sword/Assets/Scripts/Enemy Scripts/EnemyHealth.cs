using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public bool isCrystal;

    private void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Hit!");
    }
}
