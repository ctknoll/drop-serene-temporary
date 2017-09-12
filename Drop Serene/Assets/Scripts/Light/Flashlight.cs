using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Flashlight : MonoBehaviour {
	public GameObject lightObject;
	public bool lightStatus;
	public Light lt;
    public LerpControlledBob lightBob = new LerpControlledBob();

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
