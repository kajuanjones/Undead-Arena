using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newMoveStats", order = 1)]
public class PlayerMovementStats : UnitMoveStats
{
    public float jumpForce = 10;
    public float wallJumpRecoverySpeed = 3;
    public float maxWallSlideSpeed = 2;
    public float maxFallSpeed = 20;
}
