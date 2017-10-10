using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingUtils
{
    public static bool inLineOfSight (GameObject obj, GameObject target)
    {		
		RaycastHit hit;
		if (Physics.Linecast (obj.transform.position, target.transform.position, out hit))
		{
			if (hit.collider.name.Equals (target.name)) return true;
			else return false;
		}
		return true;
    }

	public static bool objectInLight (GameObject obj, Light light)
	{
		//Ambient Light Test
		if(light.GetComponent<AmbientLight>())
			return false;
		//In Spotlight angle test
		if (light.type == LightType.Spot)
			if (Vector3.Angle (light.transform.forward, obj.transform.position - light.transform.position) >= (light.spotAngle / 2))
				return false;
		//In light range test
		if (Vector3.Magnitude (obj.transform.position - light.transform.position) > light.range)
			return false;
		//Not blocked test
		if (!LightingUtils.inLineOfSight(obj, light.gameObject))
			return false;
		return true;
	}
}
