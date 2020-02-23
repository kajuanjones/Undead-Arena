using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffHandler : MonoBehaviour
{
    public float moveSpeedPercentage = 1f;
    public float attackDamagePercentage = 1f;
    public float jumpBoostPercentage = 1f;

    public List<Buff> buffs = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        buff.Apply(this);
    }

    public void RemoveBuff(Buff buff)
    {
        buff.Release(this);
        buffs.Remove(buff);
    }


    public void FixedUpdate()
    {

        foreach (Buff buff in buffs)
        {
            buff.Tick();

            if (buff.expired)
            {
                RemoveBuff(buff);
                return;
            }

        }



    }
}

[SerializeField]
public abstract class Buff
{
    public string name;
    public float duration;
    public float t;

    public bool expired = false;

    public Buff(string name = "...", float duration = 5f)
    {
        this.name = name;
        this.duration = duration;
        t = 0;
    }

    public virtual void Apply(BuffHandler buff)
    {

    }

    public void Tick()
    {

        t += Time.deltaTime;

        if (t >= duration)
        {
            expired = true;
        }
    }

    public virtual void Release(BuffHandler buff)
    {

    }
}