using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundOnCollide : MonoBehaviour {

	public AudioClip[] objectClips;

	public float minRand;
	public float maxRand;

	public AudioMixerGroup sfxOutput;

//	void OnCollisionEnter(Collision col){
//
//		if (col.gameObject.tag == "forkCollidable") {
//
//			Debug.Log ("Hit!");
//			
////			RandomizeSound(objectClips);
//
//		}

//	}

	void RandomizeSound(AudioClip[] clip)
	{

		//Randomize
		int randomClip = Random.Range (0, clip.Length);

		//Create AudioSource
		AudioSource source = gameObject.GetComponent<AudioSource>();

		// Load clip into AudioSource
		source.clip = clip[randomClip];

		//select output
		source.outputAudioMixerGroup = sfxOutput;

		//randomize pitch
		source.pitch = Random.Range (minRand, maxRand);

		//play sound
		source.Play();

		//destroy AudioSource
		//Destroy(source, source.clip.length);

	}

}
