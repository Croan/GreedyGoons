using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {

    public GameObject owner;
	public bool hitting;
	public GameObject[] targets;
	public int targetsFound;

	//start use
	public bool isUsing;
	public GameObject useTarget;
	public string useType;
	//end use

	void Start () {
        owner = transform.parent.gameObject;
		targets = new GameObject[10];
		targetsFound = 0;
		//hitting = false;
	}


	void Update () {
		if (hitting){
			if (targetsFound > 0) {
				Debug.Log ("you hit" + targets[targetsFound].gameObject.name);
				if (targets[targetsFound].GetComponent<HurtBox> ())
                {
					if (targets [targetsFound].GetComponent<HurtBox> ().hittable) 
					{
						targets [targetsFound].GetComponent<HurtBox> ().getHit ();
						ApplyHit (targets [targetsFound].GetComponent<HurtBox> ());
					}
                }
            } else
				Debug.Log ("no target...");
		}
		//start use
		if (isUsing) 
		{
			if(useTarget && useTarget.GetComponent<UseBox> ().useable)
				useType = useTarget.GetComponent<UseBox>().getUsed();
		}
		//end use
	}

	void OnTriggerEnter2D(Collider2D meep)
	{
		if (meep.gameObject.GetComponent<HurtBox> ()) {
			Debug.Log (meep.gameObject.name);
			targets [++targetsFound] = meep.transform.gameObject;
			Debug.Log ("targetsfound should have increased");
		}

		//start use
		if (meep.gameObject.GetComponent<UseBox> ()) {
			Debug.Log (meep.gameObject.name);
			Debug.Log ("found a useable object");
			useTarget = meep.transform.gameObject;
		}
		//end use
	}

	void OnTriggerExit2D(Collider2D meep)
	{
		if (meep.gameObject.GetComponent<HurtBox> ()) {
			if (meep.gameObject.name != targets [targetsFound].gameObject.name)
				targets [targetsFound - 1] = targets [targetsFound];
			targetsFound--;
		}

		//start use
		if (meep.gameObject.GetComponent<UseBox> ()) {
			useTarget.GetComponent<UseBox>().freeUp();
			useTarget = null;
			useType = "nothing";
		}
		//end use
	}
		
	public virtual void ApplyHit(HurtBox cd)
	{
	}
}
