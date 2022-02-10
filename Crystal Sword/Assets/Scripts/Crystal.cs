using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour, IDamageable
{
    public bool IsCrystal { get; set; }
    public int Health { get; set; }

    private void Start()
    {
        IsCrystal = true;
    }
    public void Damage(int damage)
    {
        Destroy(gameObject, 0.3f);
    }
    
}