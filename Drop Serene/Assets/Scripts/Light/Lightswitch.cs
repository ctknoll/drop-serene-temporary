using UnityEngine;

[RequireComponent(typeof(BoxCollider))]

public class Lightswitch : MonoBehaviour {  
    
    public Light light;    
    private bool inRange = false;

    // Use this for initialization
    void Start () {
        GetComponent<BoxCollider>().isTrigger = true;        
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Fire2") && inRange)
        {
            light.enabled = !light.enabled;
        }        
	}

    private void OnTriggerEnter(Collider other)
    {
        inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        inRange = false;
    }
}
