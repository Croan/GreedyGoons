using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTransition
{
    public string name;
    public State[] from;

    public ActionTransition(string nam, State[] frm)
    {
        name = nam;
        from = frm;
    }


    public bool IsValid(State start)
    {
        if (from.Length == 0)
        {
            return true;
        }

        foreach (State s in from)
        {
            //Debug.Log("testing is valid with " + start.name);
            //Debug.Log("testing all transitions " + s.name);
            if (start == s.badname)
            {
                return true;
            }
        }
        return false;
    }
}

