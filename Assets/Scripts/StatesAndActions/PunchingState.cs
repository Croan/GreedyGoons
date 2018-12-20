using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PunchingState : State
{
    public float startupTime;
    public float activeTime;
    public float recoveryTime;
    private Mover move;
    private HitBox hitBox;
   

    public override void EnterAction()
    {
		if (anim) {
			anim.Play ("PunchSelect");
		}
        base.EnterAction();
        print("punching");
    }

    public override void LeaveAction()
    {

        Debug.Log("stopping to punch");
        base.LeaveAction();
    }

    public override void Start()
    {
        move = GetComponent<Mover>();
        hitBox = GetComponentInChildren<HitBox>();
        base.Start();
        actions.Add(new WalkingAction(this));
        //transitions = new List<string> { typeof(IdleState).ToString() , StateNames.bad };
        //SetupTransitions(new HashSet<string> { typeof(IdleState).ToString(), Terms.bad });
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
        if (stateMachine.timeSinceLastChange < startupTime)
        {
            hitBox.GetComponent<Collider2D>().offset = 0.5f * (move.facing.normalized);
        }
        else if (stateMachine.timeSinceLastChange < startupTime + activeTime)
        {
            //hitbox stuff
            print("IM PUNCHING HERE!");
            if (hitBox != null)
            {
                hitBox.hitting = true;
				Debug.Log ("did it again");
            }

        }
        else if (stateMachine.timeSinceLastChange < startupTime + activeTime + recoveryTime)
        {
            hitBox.hitting = false;
        }
        else
        {
			if (this.GetComponent<Animator> ()) {
				this.GetComponent<Animator> ().Play ("IdleState");
			}
            stateMachine.SetNotTrue(Terms.punchingVar);
        }

    }
}
