using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonHit : HitBox {

    public override void ApplyHit(HurtBox victim)
    {
        victim.GetComponent<StateMachine>().TransitionTo(typeof(ParalyzedState).ToString());
    }

	
}
