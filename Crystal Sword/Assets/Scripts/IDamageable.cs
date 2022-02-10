using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    bool IsCrystal { get; set; }
    int Health { get; set; }
    void Damage(int damage);
}
