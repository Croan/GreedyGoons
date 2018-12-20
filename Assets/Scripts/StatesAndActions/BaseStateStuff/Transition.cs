using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum testers { equal, less, greater };
public enum types {normal,trigger, oneFrame };
public delegate bool Pred(float a, float b);

[CreateAssetMenu(fileName = "RawTransition", menuName = "Transition", order = 100)]
public class Transition : ScriptableObject
{
    static public float falseValue = 0;
    static public float trueValue = 1;
    static public float notTrueValue = -1;
    //public StateMachine machine;

    //[SerializeField]
    [StringInList(typeof(Terms), "GetStateNames")] public string[] fromStates;
    [StringInList(typeof(Terms), "GetStateNames")] public string toState;

    public TransitionCondition[] conditions;


    private void OnEnable()
    {
        if(fromStates == null)
        {
            fromStates = new string[1];
        }
        if (conditions == null) {
            conditions = new TransitionCondition[1];
        }

        foreach (var item in conditions)
        {
            //Debug.Log("cond on guy of name " + name + " , " + );

        }
    }

    public bool SetupTransition(StateMachine s)
    {
        // machine = s;
        foreach (var item in conditions)
        {
            item.SetPred();
        }
        return true; // eventually make it writeout if a value not setup or soem shit fuckit im not cut out for this shit anyways
    }

    public bool CheckAllTransitions(StateMachine s)
    {
        foreach (var item in conditions)
        {
            if (!item.CheckOneTransition(s))
            {
                return false;
            }
        }
        return true;
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






