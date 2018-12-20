using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleHit : HitBox
{
    public override void ApplyHit(HurtBox cd)
    {

        if (owner.GetComponent<AIController>() != null) // pre screen the tackle rto make sure its legit
        {
            if (owner.GetComponent<AIController>().target != cd.owner.transform && owner.GetComponent<AIController>().target == null)
            {
                return;// only tackle ur target;
            }
        }

        print(owner.name + ", " + cd.owner.name );
        owner.GetComponent<BeatingState>().CheckAndTransitionTo(cd.owner.GetComponent<StateMachine>());

		SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxCroc, transform.position, 0.7f);
		SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxGrab, transform.position, 0.3f); // FIND NEW SOUND
        //cd.processor.RecievePinned(this);

    }

}
