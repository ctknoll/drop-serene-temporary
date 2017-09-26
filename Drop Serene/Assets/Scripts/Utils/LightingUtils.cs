using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingUtils
{
    public static bool inLineOfSight (GameObject obj, GameObject target)
    {		
		RaycastHit hit;
		if (Physics.Linecast (obj.transform.position, target.transform.position, out hit) && hit.collider.name.Equals(target.name)) 
		{				
			return true;	
		}
		return false;
    }
}
