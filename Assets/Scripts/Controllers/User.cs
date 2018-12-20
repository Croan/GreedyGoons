using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User : MonoBehaviour {

	protected StateMachine stateMachine;
	public HitBox hitBox;
	public bool rowing;

	private void Start()
	{
		stateMachine = GetComponent<StateMachine>();
		hitBox = this.GetComponentInChildren<HitBox> ();
		rowing = false;
	}

	public void Use()
	{
		if (rowing) {
			print ("this is the stop rowing");
			stateMachine.SetNotTrue (Terms.rowingVar);
		} else {
			hitBox.isUsing = true;
			if (hitBox.useType == "row")
				Row ();
		}
	}
	public void StartUse()
	{
		if (hitBox.useType == "row") 
		{
			if (!rowing) {
				Row ();
				rowing = true;
			} else
				rowing = false;
		}
		hitBox.isUsing = false;
	}
	public void Row()
	{
		//stateMachine.TransitionTo(typeof(PunchingState).ToString());
		stateMachine.SetTrue(Terms.rowingVar);
	}
}
