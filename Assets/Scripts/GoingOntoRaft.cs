using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingOntoRaft : MonoBehaviour {

	public int debugDry;
    private Box box;

    private void Start()
    {
        box = Box.FindMy(BoxType.stand, BoxGender.pepee, gameObject);
        box.onEnter.AddListener(EnterWater);
        box.onExit.AddListener(ExitWater);
    }

    private void ExitWater(GameObject arg0)
    {
        var s = arg0.GetComponent<StateMachine>();
        if (s)
        {
            s.SetTrue(Terms.wetVar);
        }
    }

    private void EnterWater(GameObject arg0)
    {
        var s = arg0.GetComponent<StateMachine>();
        if (s)
        {
            s.SetFalse(Terms.wetVar);
        }
    }


    /*

    private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("entering raft");
        var s = collision.GetComponentInParent<StateMachine>(); 
		if (s) {
			print ("got here enter");
			debugDry = 1;
			s.SetFalse (Terms.wetVar);
		} else
			print ("no state machine on object " + collision.gameObject.name);
	}
	private void OnTriggerExit2D(Collider2D collision)
	{
		Debug.Log("leaving raft");
        var s =collision.GetComponentInParent<StateMachine>();
		if(s)
		{
			print ("got here leave");
			debugDry = 0;
			s.SetTrue(Terms.wetVar);
		}
	}
    */
}