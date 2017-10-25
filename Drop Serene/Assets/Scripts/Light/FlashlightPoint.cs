using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPoint : MonoBehaviour {

	public LayerMask myLayerMask;
	Camera cam;
	Vector3 point;
	GameObject rotPoint;

	void Start () 
	{
		
	}

	void Awake ()
	{
		rotPoint = transform.parent.gameObject;
		cam = gameObject.transform.parent.parent.GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 fwd = cam.transform.TransformDirection (Vector3.forward);
		RaycastHit hit;
		Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			
		if (gameObject.transform.GetChild (0).gameObject.GetComponent<Light> ().intensity > 0)
		{
			if (Physics.Raycast (ray, out hit, 1000f, myLayerMask))
			{
				point = hit.point;
			} else
			{
				point = ray.GetPoint (100f);
			}

			rotPoint.transform.rotation = Quaternion.Lerp (rotPoint.transform.rotation, (Quaternion.LookRotation (point - rotPoint.transform.position)), Time.deltaTime * 5f);
		} else
		{
			point = ray.GetPoint (100f);
			rotPoint.transform.rotation = Quaternion.Lerp (rotPoint.transform.rotation, (Quaternion.LookRotation (point - rotPoint.transform.position)), Time.deltaTime * 5f);
		}

		Debug.DrawRay (ray.origin, ray.direction * 100, Color.yellow);
	}
}
