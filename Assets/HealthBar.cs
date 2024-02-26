using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxHealth(float maxHealth)
    {
        this.maxHealth = maxHealth;
        slider.maxValue = maxHealth;
        health = maxHealth;

        slider.value = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        slider.value -= damage;
    }

    public bool IsAlive()
    {
        return health <= 0;
    }
}