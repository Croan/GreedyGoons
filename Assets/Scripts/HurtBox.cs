using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HurtBox : MonoBehaviour {

    //public HitProcessor processor;
    public GameObject owner;
	public UnityEvent onHit;
	public bool hittable;
	public float stunDuration;
	private float hitAtTime;

	// Use this for initialization
	void Start () {
        owner = transform.parent.gameObject;
        /*
        processor = transform.parent.GetComponent<HitProcessor>();
        if(processor == null)
        {
            print("no hitprocessor detected! i need one os i can get fucked up");
        }
        */
		hittable = true;
		stunDuration = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time > hitAtTime + stunDuration) 
		{
			if (hittable == false)
				Debug.Log ("you can hit it again");
			hittable = true;
		}
	}
		
	public void getHit()
	{
		if (hittable) {
			onHit.Invoke ();
			hitAtTime = Time.time;
			Debug.Log (this.gameObject.name + "got hit");
			hittable = false;
			Debug.Log ("can't hit it now!");
		}
	}
		
}
