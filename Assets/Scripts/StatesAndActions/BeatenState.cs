using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatenState : State
{

    public float liveTime;

    public override void EnterAction()
    {
		anim.Play ("GetBeatSelect");
        base.EnterAction();
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
    }

    public override void Start()
    {
        base.Start();
        //transitions = new List<string> { StateNames.idle, StateNames.dead };
        SetupTransitions(new HashSet<string> { Terms.idleState, Terms.deadState });
        //badname.Add(StateNames.bad);

    }

    public override void UpdateAction()
    {
        base.UpdateAction();
        rigid.velocity = Vector2.zero;

        if (stateMachine.timeSinceLastChange > liveTime)
        {
            stateMachine.TransitionTo(Terms.deadState);
        }
    }
}
