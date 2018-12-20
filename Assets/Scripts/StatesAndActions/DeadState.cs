using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeadState : State
{
    public UnityEvent onDeath;
    public bool dead;

    public override void EnterAction()
    {
		SoundManager.Instance.PlaySFXFromList (SoundManager.Instance.sfxHurt, transform.position, 0.7f);
		anim.Play ("DeadSelect");
        rigid.velocity = Vector2.zero;
        base.EnterAction();
        onDeath.Invoke();
        dead = true;
    }

    public override void LeaveAction()
    {
        base.LeaveAction();
    }

    public override void Start()
    {
        base.Start();
        SetupTransitions(new HashSet<string> { });
        // badname.Add(StateNames.bad);
        dead = false;
    }

    public override void UpdateAction()
    {
        base.UpdateAction();
    }
}
