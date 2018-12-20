using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
// ensure class initializer is called whenever scripts recompile
//[InitializeOnLoadAttribute]


[CreateAssetMenu(fileName = "Transitionalizer", menuName = "Transitionalizer", order = 101)]
public class TransitionInitializer : ScriptableSingleton<TransitionInitializer>
{
    public List<Transition> transitions;

    static TransitionInitializer _instance = null;
    
    public static TransitionInitializer Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = (TransitionInitializer )Resources.LoadAll("", typeof(TransitionInitializer))[0];
            }

            return _instance;
        }
    }
    



    public void RequestTransitions(State s)
    {


        List<Transition> temp = FindTransitionsFor(s).ToList();
        foreach (var item in temp)
        {
            item.SetupTransition(s.stateMachine);
        }
        s.realTransitions = temp;
    }

    public IEnumerable<Transition> FindTransitionsFor(State s)
    {
        var ret =
            from trans in transitions
            where TransitionFor(trans, s)
            select trans;

        return ret;

    }

    public bool TransitionFor(Transition t, State s)
    {
        if (t.fromStates.Contains(s.GetBadName()))
        {
            return true;
        }
        return false;
    }

#if UNITY_EDITOR
    private void OnEnable()
    {
        TheThingThatGetsCalledOnScriptReload();
    }


    [DidReloadScripts]
    public static void TheThingThatGetsCalledOnScriptReload()
    {
        //Debug.Log("starting to laod the transitions");
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(Transition).Name);  //FindAssets uses tags check documentation for more info
        Transition[] a = new Transition[guids.Length];
        //Debug.Log(a.Length);
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            //Debug.Log("loading transition " + i);
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath< Transition>(path);
        }
        //Debug.Log("reloading scriptss and the instance is " + TransitionInitializer.Instance);
        TransitionInitializer.Instance.transitions = a.ToList();
    }
#endif
}