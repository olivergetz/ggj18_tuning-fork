using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScreen : MonoBehaviour {

	public float speed = 10f;
	public int sceneIndex = 0;

	void Start(){

		StartCoroutine (LoadNewScene (sceneIndex));

	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Rotate (Vector3.up, speed * Time.deltaTime);
	
	}

	IEnumerator LoadNewScene(int sceneToLoadIndex){

		yield return new WaitForSeconds(5);

		AsyncOperation async = Application.LoadLevelAsync(sceneToLoadIndex);

		while (!async.isDone) {
			yield return null;
		}

	}

}
