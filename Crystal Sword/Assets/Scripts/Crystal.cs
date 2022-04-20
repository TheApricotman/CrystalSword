using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IDamageable
{
    public bool IsCrystal { get; set; }
    public int Health { get; set; }
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        IsCrystal = true;
    }
    public void Damage(int damage)
    {
        anim.SetTrigger("dead");
        Destroy(gameObject, 0.5f);
    }
    
}