using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	
	GameObject player;
	public float walkingDistance = 10.0f;
	public int health = 3;
	public GameObject deathParticle;

	Transform playerTransform;

	// Use this for initialization
	void Start () {

		//Find the player object in the scene. Using a prefab won't work, as the sctipt will look for the prefab itself and not an object in the scene.
		player = GameObject.Find ("Player");

		playerTransform = player.transform;

	}
	
	// Update is called once per frame
	void Update()
	{
		CheckIfDead ();

		//Look at the player
		transform.LookAt(playerTransform);
		//Calculate distance between player
		float distance = Vector3.Distance(transform.position, playerTransform.position);
		//If the distance is smaller than the walkingDistance
		if(distance < walkingDistance)
		{
			//Move the enemy towards the player
			transform.position = Vector3.Lerp(transform.position, playerTransform.position, 0.03f);
		}

	}

	void CheckIfDead (){

		if (health <= 0) {

			GameObject particleClone = Instantiate(deathParticle, transform.position, Quaternion.identity);
			Destroy (particleClone, 1f);

			Destroy (gameObject);

		}

	}

}