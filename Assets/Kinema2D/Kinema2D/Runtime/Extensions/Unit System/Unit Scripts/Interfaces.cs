using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IActivatable
{
    void Activate();
}

public interface IAttackable
{
    void Attack(Vector2 knockbackDir, int damage, float force);
}



