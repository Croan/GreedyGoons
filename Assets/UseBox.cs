using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBox : MonoBehaviour {

	public GameObject owner;
	public bool useable;
	public string useType;

	void Start () 
	{
		owner = this.transform.parent.gameObject;
		useable = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public string getUsed()
	{
		if (useable) 
		{
			print ("Using");
			useable = false;
			return "row";
		}
		else
			print ("can't use");
		return "nothing";
	}

	public void freeUp()
	{
		useable = true;
	}
}
