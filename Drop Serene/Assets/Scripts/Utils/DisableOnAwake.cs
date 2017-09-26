using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnAwake : MonoBehaviour {

	// Use this for initialization
	void Start () 
	{
		
	}

	void Awake ()
	{
		gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
