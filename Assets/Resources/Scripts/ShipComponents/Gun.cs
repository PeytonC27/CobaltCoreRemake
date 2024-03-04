using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : ShipComponent
{
    private AudioSource fireSound;

    private void Start()
    {
        fireSound = GetComponent<AudioSource>();
    }

    public bool IsStunned { get; private set; }

    public void StunGun()
    {
        IsStunned = true;
    }

    public void UnstunGun()
    {
        IsStunned = false;
    }

    public void PlayGunSound()
    {
        fireSound.Play();
    }
}
