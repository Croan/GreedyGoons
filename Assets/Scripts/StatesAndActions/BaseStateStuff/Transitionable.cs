using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Transitionable : MonoBehaviour {
    [HideInInspector]
    public string badname;
    [HideInInspector]
    public HashSet<string> transitions;

    public Transitionable()
    {
      //  transitions = new List<Transitionable>();
    }
    public Transitionable(string nam) : base()
    {
        badname = nam ;
    }

    
    //asdasd

    protected bool CheckValidTransition(string s)
    {
        /*
       string t = transitions.Find(x => x == s);
        if(t != null && t != "")
        {
            return true;
        }
        return false;*/
        return transitions.Contains(s);
    }

    public bool CanTransitionToState(string nextTransitionable)
    {
   
            if (CheckValidTransition(nextTransitionable))
            {
                return true;
            }
        
        return false;
    }
    public bool CanTransitionToState(List<string> nextTransitionable)
    {
        foreach (string s in nextTransitionable)
        {
            if (CheckValidTransition(s))
            {
                return true;
            }
        }
        return false;
    }


    public bool CheckNamesFor(IEnumerable<string> ss)
    {
        foreach (string  s in ss)
        {
           if(badname == s)// if (CheckNamesFor(s))
            {
                return true;
            }
        }
        return false;
    }
/*
    public bool CheckNamesFor(string s)
    {
        if (badname.Find((x) => x == s) != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    */
}
