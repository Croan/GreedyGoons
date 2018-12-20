using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public override void EnterAction()
    {
		if (this.GetComponent<Animator> ()) {
			this.GetComponent<Animator> ().Play ("IdleState");
		}
        rigid.velocity = Vector2.zero;
        base.EnterAction();
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
    }

    public override void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        base.Start();
        //transitions = new List<string>{ typeof(WalkingState).ToString() , typeof(PunchingState).ToString(), StateNames.bad};
        SetupTransitions(new HashSet<string> { Terms.walkState, Terms.attack, Terms.bad });
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
    }
}
