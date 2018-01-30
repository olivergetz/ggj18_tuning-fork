using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

//All player settings and controls can be found in this script

public class PlayerController : MonoBehaviour {

	public int playerMaxHealth = 5;
	public int playerCurrentHealth;

	[System.Serializable] //Make settings adjustable in inspector.
	public class MoveSettings{

		public float movementSpeed = 10f;
		public float rotationSpeed = 0.5f; //0.0 to 1.0
		public float meleeRange = 1.0f;
		public float strength = 200f;

	}

	[System.Serializable]
	public class InputSettings{

		public string UPDOWN_AXIS = "Vertical";
		public string LR_AXIS = "Horizontal";

	}

	//References to classes
	public MoveSettings moveSettings = new MoveSettings ();
	public InputSettings inputSettings = new InputSettings ();

	public Transform hitParticle;

	Rigidbody rBody;
//	Rigidbody enemyRB;

	Animator animator;

	RaycastHit hit;

	private Vector3 velocity = Vector3.zero; //Store player velocities here.
	private Quaternion playerRotation = Quaternion.identity; //Store player velocities here.

	//Variable Definitions
	float upDownInput;
	float LRInput;
	bool isMoving = false;

	//Audio Definitions
	AudioSource audioSource;

	[Range(0.0f, 1.0f)]
	public float volume;
	[Range(0.0f, 1.0f)]
	public float audioFadeTime;

	// Use this for initialization
	void Start () {

		//Check if we have a rigidbody, else throw an error.
		if (GetComponent<Rigidbody>())
			
			rBody = GetComponent<Rigidbody> ();

		else

			Debug.LogError("The player object needs a rigidbody.");

		if (GetComponent<AudioSource> ()) {

			audioSource = GetComponent<AudioSource> ();
			audioSource.volume = 0.0f;

		}else

			Debug.LogError("The player object needs an audio source.");

		if (GetComponent<Animator>())

			animator = GetComponent<Animator> ();

		else

			Debug.LogError("The player object needs an animator.");
		
	}

	void FixedUpdate(){ //Used to add force to a rigidbody every fixed frame, because it is regular.

		Move();
		Rotate ();
//		knockBackEnemy ();

		//Update Velocity
		rBody.velocity = velocity;
		rBody.rotation = playerRotation;

	}
	
	// Update is called once per frame
	void Update () {

		GetInput ();
		attack ();
		audioLoop ();
	}

	void LateUpdate(){

		knockBackEnemy ();

	}

	//Gather input data
	void GetInput(){

		upDownInput = Input.GetAxis (inputSettings.UPDOWN_AXIS); //Stores a float from -1 to 1.
		LRInput = Input.GetAxis (inputSettings.LR_AXIS); //Stores a float from -1 to 1.

		//Check if player is moving
		if ((LRInput != 0) || (upDownInput != 0)) {
			isMoving = true;
		} else {
			isMoving = false;
		}

	}

	void Move(){

		//Move based on camera view
		Vector3 rightLeft = Camera.main.transform.right * moveSettings.movementSpeed * LRInput;
		Vector3 upDown = Camera.main.transform.forward * moveSettings.movementSpeed * upDownInput;

		velocity = rightLeft + upDown;

		//Don't move on y axis.
		velocity.y = 0f;

	}

	void Rotate(){

		if (upDownInput > 0) {
			playerRotation = Quaternion.Lerp(playerRotation, Quaternion.Euler(0f,45,0f), moveSettings.rotationSpeed);
		}
		if (upDownInput < 0) {
			playerRotation = Quaternion.Lerp(playerRotation, Quaternion.Euler(0f,-135,0f), moveSettings.rotationSpeed);
		}
		if (LRInput > 0) {
			playerRotation = Quaternion.Lerp(playerRotation, Quaternion.Euler(0f,135,0f), moveSettings.rotationSpeed);
		}
		if (LRInput < 0) {
			playerRotation = Quaternion.Lerp(playerRotation, Quaternion.Euler(0f,-45,0f), moveSettings.rotationSpeed);
		}

	}

	void audioLoop (){

		if (isMoving) {

			StartCoroutine ("FadeAudioIn");

		}

		if (!isMoving) {

			StartCoroutine ("FadeAudioOut");

		}

	}

	void attack(){

		bool isAttacking = Input.GetButton("Fire1");
		animator.SetBool("isAttacking", isAttacking);


	}

	//TODO: Might want to break this
	void knockBackEnemy(){

		if (Physics.Raycast (gameObject.transform.position, gameObject.transform.forward, out hit, 2.0f) && (Input.GetButtonDown ("Fire1"))) {

			//Check if the object hit is an enemy, so our code doesn't look for a rigidbody on other objects.
			if (hit.collider.gameObject.tag == "Enemy") {
				
				if (hit.collider.gameObject.GetComponent<Rigidbody> ()) {
				
					Rigidbody enemyRB = hit.collider.gameObject.GetComponent<Rigidbody> ();
					enemyRB.AddForce (transform.forward * moveSettings.strength, ForceMode.Impulse);

				} else {

					Debug.LogError ("The enemy GameObject does not contain a Rigidbody. Please add one.");

				}

				//Reference to game object
				GameObject hitObject = hit.collider.gameObject;

				if (hitObject.GetComponent<EnemyAI> ()) {
					//Deal damage to enemy
					EnemyAI enemyAI = hitObject.GetComponent<EnemyAI> ();
					enemyAI.health -= 1;

				} else {

					Debug.LogError ("The enemy GameObject does not contain an EnemyAI script. Please add one.");

				}

			}

		}

	}

	void healthTracker(){

		//Display 

	}


	IEnumerator FadeAudioIn(){

		audioSource.volume = Mathf.Lerp(audioSource.volume, volume, audioFadeTime);

		yield return null;
	
	}

	IEnumerator FadeAudioOut(){

		audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, audioFadeTime);

		yield return null;

	}



}
