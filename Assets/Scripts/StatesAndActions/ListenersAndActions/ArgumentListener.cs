using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ArgumentListener<t> : BaseAction , IListener
{
    UnityAction<t> action;
    UnityEvent<t> eve;

    public ArgumentListener(State s, UnityAction<t> a, UnityEvent<t> e) : base(s)
    {
        action = a;
        eve = e;
    }

    void IListener.Register()
    {
        eve.AddListener(action);
    }


    void IListener.Unregister()
    {
        eve.RemoveListener(action);
    }

 
}
