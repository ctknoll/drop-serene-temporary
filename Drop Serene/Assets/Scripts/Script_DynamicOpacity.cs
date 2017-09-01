using UnityEngine;
using System.Collections;

public class Script_DynamicOpacity : MonoBehaviour {
	public GameObject player;
	GameObject thisObject;
	Renderer rend;
	Color thisColor;
	public Transform objectPos;
	public float distValue;
	public Transform playerPos;
	Color objColor;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("FirstPersonCharacter");
		rend = GetComponent<Renderer> ();
		objColor = rend.material.color;
	}
	
	// Update is called once per frame
	void Update () {
		distValue = Vector3.Distance (player.transform.position, transform.position);
		Debug.Log (distValue);

		objColor.a = (1 - distValue);
	}
}
