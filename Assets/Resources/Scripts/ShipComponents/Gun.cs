using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : ShipComponent
{

    public bool IsStunned { get; private set; }

    public void StunGun()
    {
        IsStunned = true;
    }

    public void UnstunGun()
    {
        IsStunned = false;
    }

    
}
