using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
// ensure class initializer is called whenever scripts recompile
//[InitializeOnLoadAttribute]
[CreateAssetMenu(fileName = "Transitionalizer", menuName = "Transitionalizer", order = 101)]
public class TransitionInitializer : ScriptableSingleton<TransitionInitializer>
{
    public List<Transition> transitions;

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
        if (t.fromStates.Contains(s.badname))
        {
            return true;
        }
        return false;
    }

    private static void LogPlayModeState(PlayModeStateChange state)
    {
        Debug.Log(state);
        if(state == PlayModeStateChange.ExitingEditMode)
        {
            TheThingThatGetsCalledOnScriptReload();
        }
    }
    
    [DidReloadScripts]
    public static void TheThingThatGetsCalledOnScriptReload()
    {
        Debug.Log("reloading scriptss");
        string[] guids = AssetDatabase.FindAssets("t:" + typeof(Transition).Name);  //FindAssets uses tags check documentation for more info
        Transition[] a = new Transition[guids.Length];
        for (int i = 0; i < guids.Length; i++)         //probably could get optimized 
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            a[i] = AssetDatabase.LoadAssetAtPath< Transition>(path);
        }

        TransitionInitializer.Instance.transitions = a.ToList();
    }
}