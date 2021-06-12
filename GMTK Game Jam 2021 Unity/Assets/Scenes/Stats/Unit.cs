using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth;

    public int currentHealth { get; set; }

    public event Action<float, float> onUnitTakeDamage;

    public virtual void Awake()
    {
        currentHealth = maxHealth;
    }

    public virtual bool TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);

        onUnitTakeDamage?.Invoke(currentHealth, maxHealth);

        if (currentHealth == 0)
        {
            Die();
            return true;
        }

        return false;
    }

    public virtual void Die()
    {

    }
}
