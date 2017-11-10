using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalLight : MonoBehaviour {

    public Light[] globalLights;
    public GameObject[] makeInvisible;
    public bool makeObjectsInvisible = true;

    private bool lightsOn = false;
    private bool rendererOn = true;

	// Use this for initialization
	void Start () {
        ToggleLights(lightsOn);
        ToggleRenderer(rendererOn);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.L))
        {
            lightsOn = !lightsOn;
            rendererOn = !rendererOn;
            if (makeObjectsInvisible)
                ToggleRenderer(rendererOn);
            ToggleLights(lightsOn);

        }
	}

    void ToggleLights(bool enabled)
    {
        if (globalLights.Length == 0)
            return;
        foreach(Light light in globalLights)
        {
            if(light != null)
                light.enabled = enabled;
        }
    }

    void ToggleRenderer(bool enabled)
    {
        if (makeInvisible.Length == 0)
            return;
        foreach (GameObject obj in makeInvisible)
        {
            if(obj != null)
                obj.GetComponent<MeshRenderer>().enabled = enabled;
        }
    }
}
