// Must be a child of the Player object/prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathingController : MonoBehaviour {

	public GameObject playerObject;
	float playerStamina;
	public AudioSource breathingAudio;
	public AudioClip breathingHard;
	public AudioClip breathingSoft;

	// Use this for initialization
	void Start () {
		breathingAudio = GetComponent<AudioSource> ();
		playerObject = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (!breathingAudio.isPlaying) {
			breathingAudio.Play ();
		}
		playerStamina = playerObject.GetComponent<PlayerMovement> ().stamina;
		if (playerStamina >= 1) {
			breathingAudio.clip = breathingSoft;
		} else {
			breathingAudio.clip = breathingHard;
		}
		breathingAudio.volume =(0.9f - playerStamina);
	}
}
