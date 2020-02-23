using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPlayerController : PlayerController
{

    public BuffHandler buffHandler;

    public override float MoveSpeed()
    {
        return playerMoveStats.baseMoveSpeed * buffHandler.moveSpeedPercentage;
    }

    public override float JumpForce()
    {
        return playerMoveStats.jumpForce * buffHandler.jumpBoostPercentage;
    }

}

public class JumpBuff : Buff
{
    public float jumpBoost = 0f;

    public JumpBuff(float jumpBoostPercentage, float duration, string name = "JumpBuff") : base(name, duration)
    {
        jumpBoost = jumpBoostPercentage;
    }

    public override void Apply(BuffHandler buff)
    {
        buff.jumpBoostPercentage += jumpBoost;
    }

    public override void Release(BuffHandler buff)
    {
        buff.jumpBoostPercentage -= jumpBoost;
    }
}

public class SpeedBuff : Buff
{
    float speedBoostPercentage;

    public SpeedBuff(float speedBoost, float duration, string name = "..") : base(name, duration){
        this.speedBoostPercentage = speedBoost;
    }

    public override void Apply(BuffHandler buff)
    {
        buff.moveSpeedPercentage += speedBoostPercentage;
    }

    public override void Release(BuffHandler buff)
    {
        buff.moveSpeedPercentage -= speedBoostPercentage;
    }
}


