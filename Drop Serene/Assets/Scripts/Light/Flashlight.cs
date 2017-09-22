using System.Collections;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Flashlight : MonoBehaviour
{
	public GameObject lightObject;
	public bool lightStatus;
	public Light lt;
    public LerpControlledBob lightBob = new LerpControlledBob();
    public Collider lightCollider;

	// Use this for initialization
	void Start () {
		lightStatus = false;
		lt = GetComponent<Light>();
        lightCollider = GetComponent<Collider>();
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

    void OnTriggerEnter(Collider other)
    {
        other.GetComponent<LightableObject>().isLit = true;
    }
}
