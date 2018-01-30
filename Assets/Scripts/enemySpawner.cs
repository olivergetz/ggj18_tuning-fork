using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawner : MonoBehaviour {

	//Public Variables
	public int maxEnemies = 1; //Max enemies one instance of this script is allowed to spawn.
	public float minSpawnDistance = 0; //Used to randomize spawn location slightly.
	public float maxSpawnDistance = 0; //Used to randomize spawn location slightly.

	public GameObject enemyPrefab; //The gameObject to be spawned.

	//Private Variables
	int spawnedEnemies = 0; //Keeps track of how many enemies have been spawned.
	
	// Update is called once per frame
	void Update () {

		if (spawnedEnemies < maxEnemies) {

			StartCoroutine ("spawnEnemies");

		}
		
	}

	IEnumerator spawnEnemies(){

		while (spawnedEnemies < maxEnemies) {

			Vector3 spawnLocation = new Vector3(transform.position.x + Random.Range(minSpawnDistance, maxSpawnDistance), transform.position.y - 5, transform.position.z + Random.Range(minSpawnDistance, maxSpawnDistance));

			GameObject enemyClone = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);

			spawnedEnemies += 1;

			yield return new WaitForSeconds(1f);

		}

	}

}
