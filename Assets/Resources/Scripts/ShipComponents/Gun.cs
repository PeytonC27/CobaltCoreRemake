using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : ShipComponent
{
    [SerializeField] int baseDamage = 1;
    int damageBuff = 0;

    public bool IsStunned { get; private set; }

    public int GetDamage()
    {
        return baseDamage + damageBuff;
    }

    public void AddDamage(int damage)
    {
        damageBuff = damage;
    }

    public void ResetDamageBuff()
    {
        damageBuff = 0;
    }

    public void StunGun()
    {
        IsStunned = true;
    }

    public void UnstunGun()
    {
        IsStunned = false;
    }

    
}
