using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Flashlight : MonoBehaviour
{
	public GameObject lightObject;

    [HideInInspector]
    public bool lightStatus;
    public Light lt;
    public LerpControlledBob lightBob;
    public Collider lightCollider;
    List<Collider> litObjects;

	// Use this for initialization
	void Start () {
        transform.position += Vector3.ClampMagnitude(GetComponentInParent<Transform>().forward, 1);
        lightStatus = false;
        lightCollider = GetComponent<Collider>();
        lightCollider.enabled = false;
        lt = lightObject.GetComponent<Light>();
        lightBob = new LerpControlledBob();
        litObjects = new List<Collider>();
	}
	
	// Update is called once per frame
	void Update ()
    {        
        if (!lightStatus) {
			lt.intensity = 0;
		} else {
			lt.intensity = 5;
		}
		if (Input.GetButtonDown ("Fire1"))
        {
            List<Collider> tempList = new List<Collider>(litObjects);
            foreach (Collider light in tempList)
            {
                if (lightStatus)                
                    OnTriggerExit(light);
                else
                    OnTriggerStay(light);
            }
            lightStatus = !lightStatus;
            lightCollider.enabled = lightStatus;
		}
	}

    void OnTriggerStay(Collider other)
    {        
        if (other.GetComponent<LightableObject>()/* && LightingUtils.inLineOfSight(other.gameObject, gameObject)*/)
        {
            if(!litObjects.Contains(other))
            {
                other.GetComponent<LightableObject>().LightOn();
                litObjects.Add(other);
                Debug.Log("Added " + other.name + " to light objects");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<LightableObject>()/* && LightingUtils.inLineOfSight(other.gameObject, gameObject)*/)
        {
            other.GetComponent<LightableObject>().LightOff();
            litObjects.Remove(other);
            Debug.Log("Removed " + other.name + " to light objects");
        }
    }
}
