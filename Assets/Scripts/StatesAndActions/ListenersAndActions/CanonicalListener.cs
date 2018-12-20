using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanonicalListener : BaseAction , IListener
{
    UnityAction action;
    UnityEvent eve;

    public CanonicalListener(State s, UnityAction a, UnityEvent e) : base(s)
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
