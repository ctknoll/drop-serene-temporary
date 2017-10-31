using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;

public class Flashlight : MonoBehaviour
{
	public GameObject lightObject;    
    public bool lightStatus;
    public Light lt;
    public LerpControlledBob lightBob;
    public Collider lightCollider;
    List<Collider> litObjects;
    public GameObject flashlightItem;
    public GameObject player;
	AudioSource sound;

	// Use this for initialization
	void Start () {
		sound = GetComponent<AudioSource> ();

        lightStatus = false;
        lightCollider = GetComponent<Collider>();
        lightCollider.enabled = false;
        lt = lightObject.GetComponent<Light>();
        lightBob = new LerpControlledBob();
        litObjects = new List<Collider>();        
        player = GameObject.Find("Player");
        flashlightItem = GameObject.Find("Flashlight Mesh");
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Vector3 lookDirection = flashlightItem.transform.position - (player.transform.position + player.transform.forward * 5);
        //Quaternion rotation = Quaternion.LookRotation(lookDirection);
        //flashlightItem.transform.rotation = rotation;
        if (!lightStatus)
        {
            lt.intensity = 0;
        }
        else
        {
            lt.intensity = 5;
        }
		if (Input.GetButtonDown("Fire1") && !GamestateUtilities.gamePaused())
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
			sound.Play ();
        }
    }

    void OnTriggerStay(Collider other)
    {        
        if (other.GetComponent<LightableObject>() && LightingUtils.inLineOfSight(player, other.gameObject))
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
        if (other.GetComponent<LightableObject>() && LightingUtils.inLineOfSight(player, other.gameObject))
        {
            other.GetComponent<LightableObject>().LightOff();
            litObjects.Remove(other);
            Debug.Log("Removed " + other.name + " to light objects");
        }
    }
}
