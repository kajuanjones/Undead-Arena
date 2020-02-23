using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamageSource : MonoBehaviour
{

    public int knockbackForce;
    public int damage;

    public Vector2 knockbackVector;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        IAttackable attackable = collision.GetComponent<IAttackable>();

        if (attackable != null)
        {
            attackable.Attack(knockbackVector, this.damage, this.knockbackForce);
        }
    }
}

public interface ICollisionDamageable
{
    void Damage(Vector3 knockbackDir, int Damage, int knockbackForce = 1);
    
}
