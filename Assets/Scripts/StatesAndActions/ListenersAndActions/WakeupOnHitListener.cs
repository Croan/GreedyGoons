using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WakeupOnHitListener : BaseListener 
{
    public WakeupOnHitListener(State state, UnityEvent trig) : base(state, trig)
    {
    }

    public override void ToCall()
    {
        ownerState.stateMachine.TransitionTo(Terms.idleState);
    }

 
}
