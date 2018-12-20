using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class StateEvent : UnityEvent<State> { }
[System.Serializable]
public class StateMachine : Transitionable
{
    #region variables

    public TransitionInitializer init;

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
            //Debug.Log("something went real wrong, no float of name " + name + " exists");
        }
        return f;
    }
    public bool AddFloat(string name, float f)
    {
        float cur;
        //print("the float of " + gameObject.name + " named " + name + " is being set to " + f);
        if (floatVars.TryGetValue(name,out cur))
        {
            floatVars[name] = cur + f;
            return true;
        }
        else
        {
            floatVars.Add(name, f);
            //Debug.LogError("trying to add to float val that doesnt exist");
        }
        return false;
    }
    #endregion transitionVariables



    #region init

    public virtual void Awake()
    {
        if (onStateChange == null)
            onStateChange = new StateEvent();

        lastStateChange = Time.time;

        states = new Dictionary<string, State>();
        floatVars = new Dictionary<string, float>();
        
    }
    public void Start()
    {
        foreach (var item in GetComponents<State>())
        {
           // print("adding to " + gameObject.name + ": " + item.badname);
            // states.Add((item.GetType()).ToString(), item);
            states.Add((item.GetBadName()), item);
        }
        if (!currentState)
        {
            ToDefaultState();
        }
        else
        {
            ((State)currentState).EnterAction();
        }
    }
    private void ToDefaultState()
    {
        //print("defaultstating");
        if (states.ContainsKey(Terms.idleState))
        {
            InteruptTo(Terms.idleState);
        }
        else
        {
            Debug.LogError("you have no starting state on " + gameObject.name + " and no idle state was provided so ima break now.");
        }
    }
    #endregion init
    #region transition
    public bool CheckCurrentTransitions()
    {
        foreach (Transition t in currentState.realTransitions)
        {
           // print("checking transitions for " + name + " , " + currentState.name + ", " + t.name);
            if (t.CheckAllTransitions(this))
            {
                //print(t.name + " passed");
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
            print("uh oh ! " + name + " tried to fetch but it didnt exist " + s + " (make sure the state monobehavior is on the guy in question");
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
    public void Reset()
    {

        // ToDefaultState(); this swaggily wrecks everything with a recursive loop on death

        var l = floatVars.Keys.ToList();
        foreach (var item in l)
        {
          //  print("item is " + item);
           SetFloat(item, 0);
        }
       
    }


}



