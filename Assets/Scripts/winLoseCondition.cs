using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script contains conditions for winning and losin the game.

public class winLoseCondition : MonoBehaviour {

	public int points;
	public int pointsGoal;

	// Use this for initialization
	void Start () {

		pointsGoal = 3;
		points = 0;

	}
	
	// Update is called once per frame
	void Update () {

		CheckIfWin ();
		
	}

	void CheckIfWin(){



	}
}
