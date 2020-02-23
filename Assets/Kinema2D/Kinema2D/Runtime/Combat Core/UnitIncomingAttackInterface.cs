using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent((typeof(HealthComponent)))]
[RequireComponent((typeof(UnitKnockbackComponent)))]
public class UnitIncomingAttackInterface : MonoBehaviour, ICollisionDamageable, IAttackable
{
    public HealthComponent unitHealth;
    private IKnockbackable unitKnockback;

    private void Awake()
    {
        unitKnockback = GetComponent<IKnockbackable>();
        unitHealth = GetComponent<HealthComponent>();
    }
    
    public void Damage(Vector3 knockbackDir, int damage, int knockbackForce)
    {
        unitKnockback.KnockBack(knockbackDir, knockbackForce);

        //playerHealth.Damage(damage);
        //playerHealth.Damage(damage);

        unitHealth.Damage(damage);
    }

    public void Attack(Vector2 knockbackDir, int damage, float force)
    {
        unitHealth.Damage(damage);
        unitKnockback.KnockBack(knockbackDir, force);
    }
}
