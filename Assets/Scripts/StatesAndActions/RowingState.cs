using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RowingState : State {

	public GameObject raft;
	public GameObject oar;

	public override void EnterAction ()
	{
		base.EnterAction ();
		print ("rowing");
		raft.GetComponent<RaftController> ().rowers += 1;
		oar = this.GetComponentInChildren<HitBox>().useTarget.transform.parent.gameObject;
		this.transform.position = oar.transform.position;
		print (oar.GetComponent<PaddleController> ().direction);
		anim.SetInteger ("Direction", oar.GetComponent<PaddleController> ().direction);
		if (anim) {
			anim.Play ("RowSelect");
		}
		oar.transform.localScale = new Vector3 (0, 0, 0);
		this.transform.SetParent (raft.transform);
	}

	public override void LeaveAction()
	{
		base.LeaveAction ();
		stateMachine.SetNotTrue (Terms.rowingVar);
		oar.transform.localScale = new Vector3 (1, 1, 1);
		this.transform.parent = null;
		oar = null;
		raft.GetComponent<RaftController> ().rowers -= 1;
	}

	public override void Start()
	{
		base.Start();
		SetupTransitions(new HashSet<string> {  Terms.idleState, Terms.walkState, Terms.bad});
	}

	public override void UpdateAction()
	{
		base.UpdateAction ();
		//should put something to move the raft here
	}
}
