using UnityEngine;
using System.Collections;

public class SharedCamera2D : MonoBehaviour {

	// A list of GameObjects to follow; can also be separated
	public GameObject[] objects;

	// Used in RotateCamera for added effect, but not very useful in 2D
	public float offsetRotation;
	public float rotationSpeed;

	// Offset is used for setting a camera that behaves starting from its initial placement
	private Vector3 offset;

	// These floats are used for camera tilt, which is not very usable in 2D
	private float movedLeft;
	private float movedRight;

	// Use this for initialization
	void Start () {
		//offset = transform.position - FollowedObject();
		movedLeft = 0.0f;
		movedRight = 0.0f;
	}

	// Update is called once per frame
	void FixedUpdate () {
		FollowObject ();
		// RotateCamera ();
		// For hiding objects in front of the camera, situational for 2D
		/* if (transform.position.z <= -23) {
			goalCover.GetComponent<MeshRenderer> ().enabled = false;
			goalCover.GetComponent<MeshRenderer> ().enabled = false;
		} else {
			goalCover.GetComponent<MeshRenderer> ().enabled = true;
			goalCover.GetComponent<MeshRenderer> ().enabled = true;
		} */
	}

	// Gives slight camera rotation for interesting effects, very situational for 2D
	void RotateCamera () {
		float rotate = offsetRotation * rotationSpeed * Time.deltaTime;
		if (Input.GetAxis ("Horizontal") > 0 && movedLeft < offsetRotation) {
			transform.Rotate (0.0f, -rotate, 0.0f);
			movedLeft += rotate;
		} else if (movedLeft > 0.0f) {
			transform.Rotate (0.0f, rotate, 0.0f);
			movedLeft -= rotate;
		}
		if (Input.GetAxis ("Horizontal") < 0 && movedRight < offsetRotation) {
			transform.Rotate (0.0f, rotate, 0.0f);
			movedRight += rotate;
		} else if (movedRight > 0.0f) {
			transform.Rotate (0.0f, -rotate, 0.0f);
			movedRight -= rotate;
		}
	}

	// Calculates the point at which the camera should center on
	Vector3 FollowedObject() {
		// An example for how multiple objects can be weighted differently for camera movement
		// return (player.transform.position + puck.transform.position/3.125f) / 1.32f;
		// This is used for a list of evenly weighted gameObjects
		Vector3 vectorSum = new Vector3();
		for (int j = 0; j < objects.Length; ++j)
			vectorSum += objects [j].transform.position;
		return vectorSum / objects.Length;
	}

	void FollowObject() {
		// For a smooth camera you can change the camera follow speed with this example
		/* if (transform.position != FollowedObject () + offset)
			transform.position -= (transform.position - (FollowedObject () + offset)) * 10 * Time.deltaTime; */
		// Otherwise this is a direct approach
		transform.position = FollowedObject () + new Vector3(0,0,-10);
	}
}
