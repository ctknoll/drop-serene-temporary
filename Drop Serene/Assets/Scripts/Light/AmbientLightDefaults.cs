using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientLightDefaults : MonoBehaviour 
{

	public List<Light> lights;
	public float intensity;
	public float bounce;
	public float range;
    public Color color;
	public bool useDefaultForAll;

	void Start () 
	{
        if(PlayerPrefs.GetFloat("LightLevel") != 0F)
        {
            intensity = PlayerPrefs.GetFloat("LightLevel");
        }        
    }

	void Update ()
    {
        if (useDefaultForAll)
        {
            lights.RemoveAll(light => light == null);
            foreach (Light light in lights)
            {
                light.intensity = intensity;
                light.bounceIntensity = bounce;
                light.range = range;
                light.color = color;
            }
        }
    }
}