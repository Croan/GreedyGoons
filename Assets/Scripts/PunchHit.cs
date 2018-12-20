using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHit : HitBox
{
    public float hitVelocity;

    public override void ApplyHit(HurtBox cd)
    {
        EventManager.Instance.playerPunch.Post(gameObject);
		//SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxPunch, transform.position, 0.6f);
        //cd.processor.RecievePunch(this, hitVelocity);
        cd.owner.GetComponent<StateMachine>().TransitionTo(Terms.stunState);
            cd.owner.GetComponent<Rigidbody2D>().velocity =
                ((Vector2)(cd.owner.transform.position) - 
                (Vector2)(owner.transform.position)).normalized * hitVelocity;
   
    }
}
