using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/* Use this script by adding it to an animation state. */

public class soundOnAnimation : StateMachineBehaviour {

	public AudioClip[] audioClips;
	public AudioMixerGroup output;
	[Range(0f,1f)]
	public float volume = 0.5f;
	public float minRand;
	public float maxRand;
	AudioSource audioSource;

//     OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {

		PlaySound (audioClips, animator);
	
	}

	void PlaySound (AudioClip[] clip, Animator animator){

		//Random number 0-2
		int i = Random.Range(0, clip.Length);

		//Add Audio Source
		audioSource = animator.gameObject.AddComponent<AudioSource>();

		//Add clips to audio source
		audioSource.clip = audioClips[i];

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
