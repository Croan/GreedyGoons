using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BeatingState : State
{
    public StateMachine target;
    private HitBox hitbox;
    private HurtBox hurtbox;

    private float lastTackleEnded = 0;
	private float nextPunch = 0.0f;


    public override void EnterAction()
    {
		nextPunch = 0.0f;
		anim.Play ("BeatingSelect");
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //target.TransitionTo(Terms.beatenState);
        target.SetTrue(Terms.beatenVar);
        target.transform.position = new Vector3(transform.position.x-.2f, transform.position.y-.5f, transform.position.z);
		this.GetComponent<SpriteRenderer> ().sortingOrder = target.GetComponent<SpriteRenderer>().sortingOrder + 1;

		base.EnterAction();
        hitbox.hitting = false;
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
        hitbox.hitting = true;
        
    }

    public override void Start()
    {
        base.Start();
        hitbox = GetComponentInChildren<HitBox>();
        hurtbox = GetComponentInChildren<HurtBox>();
        listeners.Add(new MethodCallListener(this, hurtbox.onHit, ReleaseTarget));
        listeners.Add(new EventMethodCallListener(this,EventGetter,ReleaseTarget));
        SetupTransitions(new HashSet<string> {Terms.idleState, Terms.bad  });

    }

    public override void UpdateAction()
    {
        base.UpdateAction();
        rigid.velocity = Vector2.zero;
		if (GetComponent<StateMachine> ().timeSinceLastChange >= nextPunch) {
			SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxPunch, transform.position, 0.4f);
			nextPunch += 0.32f;
		}
    }

    public void ReleaseTarget()
    {
        if (!target.currentState.Is(Terms.deadState))
        {
            target.GetComponent<StateMachine>().TransitionTo(Terms.idleState);
        }
        lastTackleEnded = Time.time;
        target = null;
        stateMachine.TransitionTo(Terms.idleState);     
       
    }

    public UnityEvent EventGetter()
    {
        return target.GetComponent<DeadState>().onDeath;
    }

    public bool CheckAndTransitionTo(StateMachine targ)
    {
        if (Time.time > lastTackleEnded + 1)
        {
            target = targ;
            return stateMachine.InteruptTo(this);
        }
        return false;
    }


}
