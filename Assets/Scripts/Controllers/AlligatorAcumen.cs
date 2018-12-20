using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorAcumen : BasicFollowBrain {

    protected override bool ValidTarget(GameObject targ)
    {
        StateMachine s = targ.GetComponent<StateMachine>();
        if (s != null)
        {
            if (s.currentState.CheckNamesFor(  new List<string> { Terms.deadState , Terms.beatenState} ))
            {
                print("as much as i would like to kill this person i may not because they are ndead ");
                return false;
            }
        }

            return true;
        
    }

	protected override void PlayCry() {
		SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxCroc, transform.position, 0.7f);
	}
}
