using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingState : State
{

	private Mover mover;
	public float swimSpeedMod = .5f;

    public override void EnterAction()
    {
        base.EnterAction();
        Debug.Log("swiwg");
		anim.Play ("SwimSelect");
		mover.speed = mover.speed * swimSpeedMod;
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
		mover.speed = mover.speed / swimSpeedMod;
    }

    public override void Start()
    {
        base.Start();
		SetupTransitions(new HashSet<string> { Terms.idleState, Terms.bad });
		actions.Add(new WalkingAction(this));
		mover = GetComponent<Mover> ();
    }

    public override void UpdateAction()
    {
		base.UpdateAction ();
    }
}
