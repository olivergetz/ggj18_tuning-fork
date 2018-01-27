using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

	[System.Serializable] //Make settings adjustable in inspector.
	public class MoveSettings{

		public float movementSpeed = 10f;

	}

	[System.Serializable]
	public class InputSettings{

		public string UPDOWN_AXIS = "Vertical";
		public string LR_AXIS = "Horizontal";

	}

	//References to classes
	public MoveSettings moveSettings = new MoveSettings ();
	public InputSettings inputSettings = new InputSettings ();

	Rigidbody rBody;

	private Vector3 velocity = Vector3.zero; //Store player velocities here.
	Vector3 directionOffset = Vector3.zero;
	Quaternion rotationOffset = Quaternion.Euler(0,45,0);

	//Variable Definitions
	float upDownInput;
	float LRInput;

	// Use this for initialization
	void Start () {

		//Rotate the object by 45 degrees.
//		transform.rotation = Quaternion.Euler(0,45,0);

		//Check if we have a rigidbody, else throw an error.
		if (GetComponent<Rigidbody>())
			
			rBody = GetComponent<Rigidbody> ();

		else

			Debug.LogError("The player object needs a rigidbody.");

	}

	void FixedUpdate(){ //Used to add force to a rigidbody every fixed frame, because it is regular.

		//Move based on camera view
		Vector3 rightLeft = Camera.main.transform.right * moveSettings.movementSpeed * LRInput;
		Vector3 upDown = Camera.main.transform.forward * moveSettings.movementSpeed * upDownInput;

		velocity = rightLeft + upDown;

		//Don't move on y axis.
		velocity.y = 0f;

		rBody.velocity = velocity;

	}
	
	// Update is called once per frame
	void Update () {

		GetInput ();
		
	}

	//Gather input data
	void GetInput(){

		upDownInput = Input.GetAxis (inputSettings.UPDOWN_AXIS); //Stores a float from -1 to 1.
		LRInput = Input.GetAxis (inputSettings.LR_AXIS); //Stores a float from -1 to 1.

		directionOffset.z = upDownInput;
		directionOffset.x = LRInput;


	}

	void Rotate(){

		//Use velocity to calculate rotation.


	}

}
