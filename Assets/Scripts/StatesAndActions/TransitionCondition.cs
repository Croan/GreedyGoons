using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TransitionCondition  {
    [StringInList(typeof(Terms), "GetVarNames")] public string targetVar;
    public float targetValue;
    public testers comp;
    public Pred pred;

    public types trigger;

    public void SetPred()
    {
        //Debug.Log("setting pred to " + comp);
        if (comp == testers.equal)
        {
            pred = TransitionCondition.Equal;
        }
        else if (comp == testers.greater)
        {
            pred = TransitionCondition.Greater;
        }
        else if (comp == testers.less)
        {
            pred = TransitionCondition.Less;
        }
    }


    public bool CheckOneTransition(StateMachine s)
    {
        float f = s.GetFloat(targetVar);
        if (trigger == types.oneFrame) { s.SetFloat(targetVar, Transition.falseValue); }
        if (f > Mathf.NegativeInfinity)
        {
            if (pred(f, targetValue))
            {
                if (trigger == types.trigger) { s.SetFloat(targetVar, Transition.falseValue); }

                return true;
            }
            return false;
        }
        else
        {
           // Debug.Log("transition from " + s.currentState.badname + ", to " + toState + " failed as no variable named " + targetVar + " exists on statemachine ");
            return false;
        }
    }


    static bool Equal(float a, float b)
    {
        return (Mathf.Approximately(a, b));
    }
    static bool Greater(float a, float b)
    {
        return (a > b);
    }
    static bool Less(float a, float b)
    {
        return (a < b);
    }
}
