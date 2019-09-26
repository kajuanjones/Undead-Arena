using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEventHandler : MonoBehaviour
{

    public UnityEvent attackFinish;
    public UnityEvent attackStart;
    public UnityEvent shieldedStart;
    public UnityEvent footStep;

    // Start is called before the first frame update
    public void FinishAttacking(){
        attackFinish.Invoke();
    }

    public void Shielded(){
        shieldedStart.Invoke();
    }

    public void AttackStart(){
        attackStart.Invoke();
    }

    public void FootStep(){
        footStep.Invoke();
    }
}
