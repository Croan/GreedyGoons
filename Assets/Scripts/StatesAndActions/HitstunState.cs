using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitstunState : State
{
    public float stunTime;

    public override void EnterAction()
    {
        print(GetComponent<Rigidbody2D>().velocity.magnitude);
        base.EnterAction();
    }

    public override void LeaveAction()
    {
        rigid.velocity = Vector2.zero;
        base.LeaveAction();
    }

    public override void Start()
    {
        base.Start();
        //transitions = new List<string> { typeof(IdleState).ToString() };
        SetupTransitions(new HashSet<string> { Terms.idleState, Terms.deadState });
        //badname.Add(StateNames.bad);
    }

    public override void UpdateAction()
    {
        print("ow im stunned!");
        base.UpdateAction();
        if (stateMachine.timeSinceLastChange > stunTime)
        {
            stateMachine.TransitionTo(Terms.idleState);
        }
    }

    private State ExitState()
    {
        if (stateMachine.GetState(Terms.deadState))
        {
            if (GetComponent<DeadState>().dead)
            {
                return stateMachine.GetState(Terms.deadState);
            }
        }
        return stateMachine.GetState(Terms.idleState);
    }

}
