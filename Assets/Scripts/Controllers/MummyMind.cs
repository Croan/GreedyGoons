using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyMind : BasicFollowBrain
{

    protected override bool ValidTarget(GameObject targ)
    {
        StateMachine s = targ.GetComponent<StateMachine>();
        if (s != null)
        {
            if (s.currentState.Is(typeof(DeadState).ToString()))
            {
                return false;
            }
        }

        return true;
    }

	protected override void PlayCry() {
        EventManager.Instance.mummyCry.Post(gameObject);
        //SoundManager.Instance.PlaySFX (SoundManager.Instance.sfxMummy, transform.position, 0.5f);
	}

}
