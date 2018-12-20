using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAction : BaseAction, IAction {
    private Mover mover;
    private Rigidbody2D rigid;
	private float nextStep = 0;

   // private float cycleLength = 0.3f;

    //private Transform rend;

    public WalkingAction(State s) : base(s)
    {
        rigid = ownerState.GetComponent<Rigidbody2D>();
        mover = ownerState.GetComponent<Mover>();
        //rend = ownerState.GetComponentInChildren<SpriteRenderer>().gameObject.transform;
    }

    public void EnterAct()
    {
		nextStep = 0.0f;
    }

    public void LeaveAct()
    {
        
    }

    public void UpdateAct()
    {
        rigid.velocity = mover.direction * mover.speed;
        //rend.transform.powwwwwwwwwwwwwwwsition = new Vector3(0, Mathf.Cos(ownerState.stateMachine.timeSinceLastChange), 0);
		if (mover.direction != Vector2.zero && ownerState.stateMachine.timeSinceLastChange >= nextStep) {
			SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxStep, ownerState.transform.position, 0.1f);
            //nextStep += 0.3f;
            nextStep += 0.6f/Mathf.Sqrt(mover.speed);
		}
    }

 
}
