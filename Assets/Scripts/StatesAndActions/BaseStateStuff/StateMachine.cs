using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StateEvent : UnityEvent<State> { }



[System.Serializable]
public class StateMachine : Transitionable
{
    #region variables



     public Dictionary<string, State> states;

    public StateEvent onStateChange;

    [UnityEngine.SerializeField]
    public State currentState;


    [HideInInspector]
    public float lastStateChange = 0;
    public float timeSinceLastChange;

    #endregion variables

    #region transitionVariables
    [SerializeField]
    Dictionary<string, float> floatVars;
    public bool SetFloat(string name, float f)
    {
        
        //print("the float of " + gameObject.name + " named " + name + " is being set to " + f);
        if (floatVars.ContainsKey(name))
        {
            floatVars[name] = f;
            return true;
        }
        else
        {
            floatVars.Add(name, f);
        }
        return false;
    }
    public bool SetTrue(string name)
    {
        return SetFloat(name, Transition.trueValue);
    }
    public bool SetNotTrue(string name)
    {
        return SetFloat(name, Transition.notTrueValue);
    }
    public bool SetFalse(string name)
    {
        return SetFloat(name, Transition.falseValue);
    }
    public float GetFloat(string name)
    {
        float f = Mathf.NegativeInfinity;

        if (!floatVars.TryGetValue(name, out f))
        {
            Debug.Log("something went real wrong, no float of name " + name + " exists");
        }
        return f;
    }
    /*
    Dictionary<string, int> intVars;

    public bool SetInt(string name, int f)
    {
        if (intVars.ContainsKey(name))
        {
            intVars[name] = f;
            return true;
        }
        return false;
    }
   public int GetInt(string name)
    {
        int f = -7777;

        if (!intVars.TryGetValue(name, out f))
        {
            Debug.Log("something went real wrong, no float of name " + name + "exists");
        }
        return f;
    }

    Dictionary<string, bool> boolVars;
    public bool SetBool(string name, bool f)
    {
        if (boolVars.ContainsKey(name))
        {
            boolVars[name] = f;
            return true;
        }
        return false;
    }
    public bool GetBool(string name)
    {
        bool f = false;

        if (!boolVars.TryGetValue(name, out f))
        {
            Debug.Log("something went real wrong, no float of name " + name + "exists");
        }
        return f;
    }

    Dictionary<string, bool> triggerVars;
    public bool SetTrigger(string name, bool f)
    {
        if (boolVars.ContainsKey(name))
        {
            boolVars[name] = f;
            return true;
        }
        return false;
    }
    public bool GetTrigger(string name)
    {
        bool f = false;

        if (!boolVars.TryGetValue(name, out f))
        {
            Debug.Log("something went real wrong, no float of name " + name + "exists");
        }
        return f;
    }
    */
    #endregion transitionVariables



    #region init

    public virtual void Awake()
    {
        if (onStateChange == null)
            onStateChange = new StateEvent();

        lastStateChange = Time.time;

        states = new Dictionary<string, State>();
        floatVars = new Dictionary<string, float>();
        foreach (var item in GetComponents<State>())
        {
            states.Add((item.GetType()).ToString(), item);
        }
    }

    public void Start()
    {

        if (!currentState)
        {
            if (states.ContainsKey(Terms.idleState))
            {
                InteruptTo(Terms.idleState);
            }
            else
            {
                Debug.LogError("you have no starting state on " + gameObject.name + " and no idle state was provided so ima break now.");
            }
        }
        ((State)currentState).EnterAction();
    }

    #endregion init


    #region transition

    public bool CheckCurrentTransitions()
    {
        foreach (Transition t in currentState.realTransitions)
        {
            if (t.CheckAllTransitions(this))
            {
                InteruptTo(t.toState);

                return true;

            }
            else
            {

            }
        }
        return false;

    }


    public bool InteruptTo(string s)
    {
        return InteruptTo(GetState(s));
    }

    public bool InteruptTo(State s)
    {
        // print("tryna interupt " + s.badname);
        return GoTo(s);
    }

    public State GetState(string s)
    {
        State state;
        if (states.TryGetValue(s, out state))
        {
            return state;
        }
        else
        {
            print("uh oh u fucked something up mate! u tried to fetch the followingf but it didnt exist " + s + " (make sure the state monobehavior is on the guy in question");
        }
        return null;
    }

    public bool TransitionTo(string s)
    {
        return TransitionTo(GetState(s));
    }

    public bool TransitionTo(State s)
    {
        // print("attempting a legal transition to " + s.badname);
        if (currentState == null) { return true; }
        if (currentState == s) { return false; }
        if (!currentState.CanTransitionToState(s.badname))
        {
            return false;// this is what controls allowed transitions. 
        }
        return GoTo(s);
    }


    private bool GoTo(State nextTransitionable)
    {
        if (nextTransitionable == null)
            return false;
        if (currentState == nextTransitionable)
            return false;
        //print("transitioning to " + nextTransitionable);

        lastStateChange = Time.time;
        onStateChange.Invoke((State)nextTransitionable);
        if (currentState is State && currentState != null)
        {
            ((State)currentState).LeaveAction();
        }

        currentState = nextTransitionable;

        if (currentState is State)
        {
            ((State)nextTransitionable).EnterAction();
        }
        return true;
    }





    #endregion transition


    #region updates
    public bool debuggingModeACTIVATE;

    public List<Pair> readoutValues;
    public virtual void Update()
    {
        DebuguModu();

        CheckCurrentTransitions();
        //timeSinceLastChange.SetVal(Time.time - lastStateChange);
        timeSinceLastChange = Time.time - lastStateChange;
        State s = (State)currentState;

        if (s != null)
        {
            s.UpdateAction();
        }
    }

    private void DebuguModu()
    {
        if (debuggingModeACTIVATE)
        {
            readoutValues = new List<Pair>();
            foreach (var item in floatVars)
            {
                readoutValues.Add(new Pair(item.Key, item.Value));
            }
        }
        else
        {
            //readoutValues.Clear();
        }
    }


    #endregion updates

    #region helpers
    public bool CheckCurrentState(string s)
    {
        return (currentState.CheckNamesFor(State.BreakString(s)));
    }
    #endregion helpers



}



