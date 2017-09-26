using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingUtils
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static bool inLineOfSight (GameObject obj, GameObject target)
    {
		if (Physics.Linecast(obj.transform.position, target.transform.position, LayerMask.GetMask("Blocks Line of Sight")))
			return false;
        return true;
    }
}
