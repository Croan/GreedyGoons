using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MethodCallListener : BaseListener
{
    private UnityAction func;
    public MethodCallListener(State state, UnityEvent trig, UnityAction f) : base(state, trig)
    {
        func = f;
    }

    public override void ToCall()
    {
        func.Invoke();
    }
}
