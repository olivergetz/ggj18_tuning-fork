using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameObject healthFull;
	public GameObject healthEmpty;

	float maxHealth;
	float curHealth;

	GameObject player;
	GameObject uiObject;
	GameObject healthFullClone;
	GameObject healthEmptyClone;
	PlayerController playerController;

	// Use this for initialization
	void Start () {

		//Initialize UI
		initUI ();
		
	}
	
	// Update is called once per frame
	void Update () {

		healthTanks ();
		
	}

	void initUI(){

		float screenWidth = (float)Screen.width;
		float screenHeight = (float)Screen.height;

		player = GameObject.Find ("Player");
		playerController = player.GetComponent<PlayerController> ();

		uiObject = GameObject.Find ("UI");

		maxHealth = playerController.playerMaxHealth;
		curHealth = playerController.playerCurrentHealth;

		//Display health tanks based on player health
		healthFullClone = Instantiate(healthFull);
		healthFullClone.transform.SetParent (uiObject.transform);

		healthFullClone.transform.position = new Vector3 (30, screenHeight - 30);

	}

	//Instantiate game object with health UI only once when damage is taken.
	void healthTanks(){

		//Instantiate health based on player health


	}
		

}
