using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParalyzedState : State
{

    private HurtBox detector;

    public override void EnterAction()
    {
        rigid.velocity = Vector2.zero;
        base.EnterAction();
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
    }

    public override void Start()
    {
        detector = GetComponentInChildren<HurtBox>();
        base.Start();
        //transitions = new List<string> { typeof(IdleState).ToString() };
        SetupTransitions(new HashSet<string> { typeof(IdleState).ToString() });
        // listeners.Add(new WakeupOnHitListener(this, detector.onHit));
        listeners.Add(new MethodCallListener(this, detector.onHit, WakeUp));
        // badname.Add(StateNames.bad);
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
    }

    public void WakeUp()
    {
        stateMachine.TransitionTo(Terms.idleState);
    }
}
