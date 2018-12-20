using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAction {

    public State ownerState;
    public string name;
	public BaseAction(State s)
    {
        ownerState = s;
    }
}
