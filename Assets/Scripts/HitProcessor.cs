using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitProcessor : MonoBehaviour {
    private StateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    public bool RecievePunch(HitBox hitter , float force)
    {
        if (stateMachine.TransitionTo(Terms.stunState))
        {
            GetComponent<Rigidbody2D>().velocity = 
                ((Vector2)(transform.position) - (Vector2)(hitter.owner.transform.position)).normalized * force;
            return true;
        }
        return false;
    }


    public bool RecieveDeath(HitBox hitter)
    {
        if (stateMachine.CheckCurrentState(Terms.swimState))
        {
            return (stateMachine.TransitionTo(Terms.drownState));
        }
        else
        {
            return (stateMachine.TransitionTo(Terms.deadState));
        }
    }

    public  bool RecievePinned(HitBox hitter )
    {
        if (stateMachine.CheckCurrentState(Terms.swimState))
        {

        }
        else
        {

        }

        return GetBeat(hitter);
    }

    private bool GetBeat(HitBox hitter)
    {
        return (hitter.GetComponent<BeatingState>().CheckAndTransitionTo(stateMachine));
    }

    public bool RecieveParalyzed(HitBox hitter )
    {
        return false;
    }


   

}
