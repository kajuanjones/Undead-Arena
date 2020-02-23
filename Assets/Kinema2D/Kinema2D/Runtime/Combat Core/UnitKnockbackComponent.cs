using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnitMovement))]
public class UnitKnockbackComponent : MonoBehaviour, IKnockbackable
{
    private void Awake()
    {
        this.thisUnit = GetComponent<UnitMovement>();
    }

    private UnitMovement thisUnit;

    public void KnockBack(Vector2 launchVector, float knockBackForce)
    {
        thisUnit.SetMoveState(new KnockBackState(thisUnit, launchVector, knockBackForce));
    }
}

public interface IKnockbackable
{
    void KnockBack(Vector2 launchVector, float knockBackForce);
    
}
