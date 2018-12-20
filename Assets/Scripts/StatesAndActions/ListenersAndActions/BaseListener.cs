using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseListener : IListener {
    public UnityEvent trigger;
    public State ownerState;

    public BaseListener(State state, UnityEvent trig)
    {
        ownerState = state;
        trigger = trig;
    }

    public virtual void Register()
    {
        trigger.AddListener(ToCall);
    }

    public virtual void Unregister()
    {
        trigger.RemoveListener(ToCall);
    }

    public abstract void ToCall();

}
