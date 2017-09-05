using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLight : MonoBehaviour {

	public bool overrideConfigurationWithDefaults;

	// Use this for initialization
	void Start () 
	{
		AmbientLightDefaults defaults = GameObject.Find ("__MASTER__").GetComponent<AmbientLightDefaults>();
		Light light = gameObject.GetComponent<Light>();
		if (overrideConfigurationWithDefaults)
		{
			light.intensity = defaults.intensity;
			light.bounceIntensity = defaults.bounce;
		}
	}
	
	// Update is called once per frame
	void Update () {}
}
