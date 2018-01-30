using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/* This script plays a sound based on the tag of an object in front of the player at the start of an animator state. */

public class soundOnAnimationRay : StateMachineBehaviour {

	public AudioClip[] audioClips;
	public AudioClip[] soundHitMetal;
	public AudioClip[] soundHitRubber;
	public AudioClip[] soundHitWood;
	public AudioMixerGroup output;
	[Range(0f,1f)]
	public float volume = 0.5f;
	public float minRand;
	public float maxRand;

	public GameObject hitParticle;
	public GameObject player;

	string materialTag;

	AudioSource audioSource;

	RaycastHit hit;

	//     OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		if (Physics.Raycast(animator.gameObject.transform.position, animator.gameObject.transform.forward, out hit, 2.0f)){

			materialTag = hit.collider.gameObject.tag;

			switch (materialTag)
			{
			case "Enemy":
				PlaySound (audioClips, animator);
				break;
			case "Metal":
				PlaySound (soundHitMetal, animator);
				break;
			case "Wood":
				PlaySound (soundHitWood, animator);
				break;
			case "Rubber":
				PlaySound (soundHitRubber, animator);
				break;
			}

			GameObject particleClone = Instantiate(hitParticle, hit.collider.gameObject.transform.position, Quaternion.identity);

			Destroy (particleClone, 1f);

		}

	}

	void PlaySound (AudioClip[] clip, Animator animator){

		//Random number 0-2
		int i = Random.Range(0, clip.Length);

		//Add Audio Source
		audioSource = animator.gameObject.AddComponent<AudioSource>();

		//Add clips to audio source
		audioSource.clip = clip[i];

		//set output
		audioSource.outputAudioMixerGroup = output;

		audioSource.volume = volume;

		audioSource.Play();

		//randomize pitch
		audioSource.pitch = Random.Range (minRand, maxRand);

		//Destroy source
		Destroy(audioSource, audioSource.clip.length);

	}
}
