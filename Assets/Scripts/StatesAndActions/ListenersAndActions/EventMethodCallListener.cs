using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public delegate UnityEvent EventGetter();

public class EventMethodCallListener : MethodCallListener
{
    public EventGetter eventGetter;
    public EventMethodCallListener(State state, EventGetter eG, UnityAction f) : base(state, null, f)
    {
        eventGetter = eG;
    }

    public override void Register()
    {
        trigger = eventGetter.Invoke();
        base.Register();
    }

}