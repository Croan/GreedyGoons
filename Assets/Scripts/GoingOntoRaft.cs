using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoingOntoRaft : MonoBehaviour {

	public int debugDry;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		Debug.Log("entering raft");
		var s = collision.GetComponentInParent<StateMachine>(); 
		if (s) {
			print ("got here enter");
			debugDry = 1;
			s.SetTrue (Terms.dryVar);
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
			s.SetFalse(Terms.dryVar);
		}
	}

}