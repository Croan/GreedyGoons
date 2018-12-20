using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingState : State {

    private Mover mover;

    public override void EnterAction()
    {
		if (this.GetComponent<Animator> ()) 
		{
			this.GetComponent<Animator> ().Play ("WalkSelect");
		}
		base.EnterAction();
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
    }

    public override void Start()
    {
        base.Start();
        //transitions = new List<string> { typeof(IdleState).ToString() , typeof(PunchingState).ToString() , StateNames.bad};
        //SetupTransitions(new HashSet<string> { Terms.idleState, Terms.attack, Terms.bad });
        actions.Add(new WalkingAction(this));
        mover = GetComponent<Mover>();
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
        if (mover.direction == Vector2.zero)
        {
            //stateMachine.TransitionTo(typeof(IdleState).ToString());
        }
    }
}
