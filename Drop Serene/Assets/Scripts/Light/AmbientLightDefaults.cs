using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightDefaults : MonoBehaviour 
{

	public List<Light> lights;
	public float intensity;
	public float bounce;
	public float range;
	public bool useDefaultForAll;

	void Start () 
	{
		if (useDefaultForAll) 
		{
			foreach (Light light in lights) 
			{
				light.intensity = intensity;
				light.bounceIntensity = bounce;
				light.range = range;
			}
		}
	}

	void Update () {}
}