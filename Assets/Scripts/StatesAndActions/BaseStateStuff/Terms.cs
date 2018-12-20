using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.Reflection;
using System.Linq;

public static class Terms {

    public static string walkingVar = "walkingVar";
    public static string wetVar = "wetVar";
	public static string dryVar = "dryVar";
    public static string punchingVar = "punchVar";
	public static string rowingVar = "rowVar";
    public static string getHitVar = "getHitVar";
    public static string beatenVar = "beatenVar";


    public static string idleState = typeof(IdleState).ToString();
    public static string walkState = typeof(WalkingState).ToString();
    public static string swimState = typeof(SwimmingState).ToString();
    public static string punchState = typeof(PunchingState).ToString();
	public static string rowState = typeof(RowingState).ToString();
	public static string beatingState = typeof(BeatingState).ToString();
    public static string deadState = typeof(DeadState).ToString();
    public static string drownState = typeof(DrownState).ToString();
    public static string beatenState = typeof(BeatenState).ToString();
    public static string stunState = typeof(HitstunState).ToString();
    public static string paralyzeState = typeof(ParalyzedState).ToString();

    public static string attack = punchState + "," + beatingState + ",multi";
    public static string bad = deadState + "," + beatenState + "," + stunState + "," + paralyzeState + ",multi";
    //public static string drown = dead + "," + beaten + "," + paralyze;

    public static string[] GetStateNames()
    {
        return GetNames("State");
    }

    public static string[] GetVarNames()
    {
        return GetNames("Var");
    }

    public static string[] GetNames(string s)
    {
        BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance;

        var fieldValues =
                        from fields in typeof(Terms).GetFields(bindingFlags)
                        where fields.GetValue(typeof(Terms)).ToString().EndsWith(s)
                        //where fields.Name.EndsWith(s)
                        select fields.GetValue(typeof(Terms)).ToString();


        return fieldValues.ToArray();
    }
}
