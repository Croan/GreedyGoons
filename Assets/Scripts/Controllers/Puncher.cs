using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puncher : MonoBehaviour {

    protected StateMachine stateMachine;

    private void Start()
    {
        stateMachine = GetComponent<StateMachine>();
    }

    public void Punch()
    {
        //stateMachine.TransitionTo(typeof(PunchingState).ToString());
        stateMachine.SetTrue(Terms.punchingVar);
    }


}
