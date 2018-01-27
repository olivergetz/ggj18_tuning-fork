using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isometricCamera : MonoBehaviour {

	public Vector3 posOffset = new Vector3 (0, 3, -8);
	public Quaternion rotOffset;
	public GameObject playerObject;

	float rotateVel = 0;

	Vector3 destination = Vector3.zero; //This is where we store the target destination of our camera.
	Vector3 distance;

	Transform target;

	[Range(0.01f,1.0f)]
	public float smooth = 0.5f;

	// Use this for initialization
	void Start () {

		//Initialize Camera Target
		if (playerObject != null)

			target = playerObject.GetComponent<Transform> ();

		 else 

			Debug.LogError ("Your camera needs a target.");

		distance = transform.position - target.position;
		
	}
	
	// Update is called regularly.
	void FixedUpdate () {

		//Update Camera Position and Rotation
//		lookAtTarget ();
		moveToTarget ();
		
	}

	void moveToTarget(){

		destination = playerObject.transform.position + distance + posOffset;
		transform.position = Vector3.Slerp (transform.position, destination, smooth);

	}

	void lookAtTarget(){

		float eulerYangle = Mathf.SmoothDampAngle (transform.rotation.y, target.rotation.y, ref rotateVel, 0.09f);
		transform.rotation = Quaternion.Euler (transform.rotation.x, eulerYangle, transform.rotation.z);

	}

}
