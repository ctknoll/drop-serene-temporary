using UnityEngine;
using System.Collections;

public class Script_Flashlight : MonoBehaviour {
	public GameObject lightObject;
	public bool lightStatus;
	public Light lt;

	// Use this for initialization
	void Start () {
		lightStatus = false;
		lt = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!lightStatus) {
			lt.intensity = 0;
		} else {
			lt.intensity = 5;
		}
		if (Input.GetButtonDown ("Fire1")){
			lightStatus = !lightStatus;
		}
	}
}
