using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour {

	public float speed = 0.01f;
	public int rowers = 0;
	private Rigidbody2D rigid;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		rigid = this.GetComponent<Rigidbody2D> ();
		direction.x = 0.0f;
		direction.y = speed * rowers;
		direction.z = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		direction.y = speed*rowers;
		this.transform.Translate(direction);
		print ("my speed is " + speed);
	}
}
